using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using TodoApp.Http;

namespace TodoApp.Bootstrap;

public class AggregateCondition : IHttpRequestCondition
{
  private readonly IHttpRequestCondition[] _conditions;

  public AggregateCondition(params IHttpRequestCondition[] conditions)
  {
    _conditions = conditions;
  }

  public static IHttpRequestCondition ConsistingOf(params IHttpRequestCondition[] conditions)
  {
    return new AggregateCondition(conditions);
  }

  public void Assert(HttpRequest request)
  {
    foreach (var condition in _conditions)
    {
      condition.Assert(request);
    }
  }
}

public class HttpRequestCompletenessValidatingEndpoint : IAsyncEndpoint
{
  private readonly IAsyncEndpoint _next;
  private readonly IHttpRequestCondition _httpRequestCondition;
  private readonly IEndpointsSupport _support;

  public HttpRequestCompletenessValidatingEndpoint(IHttpRequestCondition httpRequestCondition,
    IEndpointsSupport support, IAsyncEndpoint next)
  {
    _next = next;
    _httpRequestCondition = httpRequestCondition;
    _support = support;
  }

  public async Task HandleAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    try
    {
      _httpRequestCondition.Assert(request);
      await _next.HandleAsync(request, response, cancellationToken);
    }
    catch (Exception e) //bug make all exceptions inherit some sort of validation exception
    //bug make catch exception a fallback for unknown exceptions
    {
      _support.BadRequest(this, e);
      await Results.BadRequest(e /* bug do not include the exception here! */).ExecuteAsync(request.HttpContext);
    }
  }
}