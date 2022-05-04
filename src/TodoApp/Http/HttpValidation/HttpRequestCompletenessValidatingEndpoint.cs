using System.Threading;
using System.Threading.Tasks;
using HttpContextExtensions;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.Flow;
using TodoApp.Http.Support;

namespace TodoApp.Http.HttpValidation;

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
    {
      _support.BadRequest(this, e);
      await Results.BadRequest(e.ToString() /* bug do not include the exception here! */).ExecuteAsync(request.HttpContext);
    }
  }
}