using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.Flow;
using TodoApp.Http.Support;

namespace TodoApp.Http.HttpValidation;

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

  public async Task Handle(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    try
    {
      _httpRequestCondition.Assert(request);
      await _next.Handle(request, response, cancellationToken);
    }
    catch (HttpRequestInvalidException e) //bug make all exceptions inherit some sort of validation exception
    //bug make catch exception a fallback for unknown exceptions
    {
      _support.BadRequest(this, e);
      await Results.BadRequest(e.ToString() /* bug do not include the exception here! */).ExecuteAsync(request.HttpContext);
    }
  }
}