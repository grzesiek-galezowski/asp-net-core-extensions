using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Http;

namespace TodoApp.Bootstrap;

public class LogScopedEndpoint : IAsyncEndpoint
{
  private readonly IAsyncEndpoint _next;
  private readonly string _operationName;
  private readonly IEndpointsSupport _support;

  public LogScopedEndpoint(
    string operationName,
    IAsyncEndpoint next,
    IEndpointsSupport serviceSupport)
  {
    _next = next;
    _operationName = operationName;
    _support = serviceSupport;
  }

  public async Task HandleAsync(
    HttpRequest request,
    HttpResponse response,
    CancellationToken cancellationToken)
  {
    using (_support.BeginScope(this, _operationName)) //bug request id, correlationId - maybe through open telemetry?
    {
      await _next.HandleAsync(request, response, cancellationToken);
      //bug in-memory logger (e.g. Logging.Memory nuget)n
    }
  }
}