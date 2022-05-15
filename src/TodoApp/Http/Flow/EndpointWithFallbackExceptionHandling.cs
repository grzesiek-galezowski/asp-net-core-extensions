using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.Support;

namespace TodoApp.Http.Flow;

public class EndpointWithFallbackExceptionHandling : IAsyncEndpoint
{
  private readonly IEndpointsSupport _support;
  private readonly IAsyncEndpoint _next;

  public EndpointWithFallbackExceptionHandling(IEndpointsSupport support, IAsyncEndpoint next)
  {
    _support = support;
    _next = next;
  }

  public async Task Handle(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    try
    {
      await _next.Handle(request, response, cancellationToken);
    }
    catch (Exception e)
    {
      _support.UnhandledException(this, e);
      //bug make some kind of special response. Think about ditching raw HttpRequest/Response for own types
      await Results.StatusCode((int)HttpStatusCode.InternalServerError).ExecuteAsync(request.HttpContext);
    }
  }
}