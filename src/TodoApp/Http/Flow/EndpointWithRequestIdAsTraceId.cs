using System;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.Flow;

public class EndpointWithRequestIdAsTraceId : IAsyncEndpoint
{
  private readonly IAsyncEndpoint _next;

  public EndpointWithRequestIdAsTraceId(IAsyncEndpoint next)
  {
    _next = next;
  }

  public async Task Handle(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    request.HttpContext.TraceIdentifier = Guid.NewGuid().ToString();
    await _next.Handle(request, response, cancellationToken);
  }
}