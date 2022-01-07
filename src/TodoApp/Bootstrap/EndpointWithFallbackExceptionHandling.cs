using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Http;

namespace TodoApp.Bootstrap;

public class EndpointWithFallbackExceptionHandling : IAsyncEndpoint
{
  private readonly IEndpointsSupport _support;
  private readonly IAsyncEndpoint _next;

  public EndpointWithFallbackExceptionHandling(IEndpointsSupport support, IAsyncEndpoint next)
  {
    _support = support;
    _next = next;
  }

  public async Task HandleAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    try
    {
      await _next.HandleAsync(request, response, cancellationToken);
    }
    catch (Exception e)
    {
      _support.UnhandledException(this, e);
      //bug make some kind of special response. Think about ditching raw HttpRequest/Response for own types
      await Results.StatusCode((int)HttpStatusCode.InternalServerError).ExecuteAsync(request.HttpContext);
    }
  }
}