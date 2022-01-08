using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.Support;

namespace TodoApp.Http.Flow;

public class EndpointWithSupportScope : IAsyncEndpoint
{
  private readonly IAsyncEndpoint _next;
  private readonly IEndpointsSupport _support;
  private readonly ILoggedPropertySet _loggedPropertySet;

  public EndpointWithSupportScope(
    ILoggedPropertySet loggedPropertySet,
    IEndpointsSupport serviceSupport, 
    IAsyncEndpoint next)
  {
    _next = next;
    _support = serviceSupport;
    _loggedPropertySet = loggedPropertySet;
  }

  public async Task HandleAsync(
    HttpRequest request,
    HttpResponse response,
    CancellationToken cancellationToken)
  {
    using (_support.BeginScope(this, _loggedPropertySet.ToDictionaryUsing(request))) //bug also: telemetry
    {
      await _next.HandleAsync(request, response, cancellationToken);
      //bug in-memory logger (e.g. Logging.Memory nuget)n
    }
  }
}