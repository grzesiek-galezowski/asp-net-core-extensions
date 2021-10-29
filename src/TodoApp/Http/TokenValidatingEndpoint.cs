using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http;

public class TokenValidatingEndpoint : IAsyncEndpoint
{
  public const string RequiredToken = "trolololo";
    
  private readonly IAsyncEndpoint _nestedAction;

  public TokenValidatingEndpoint(IAsyncEndpoint nestedAction)
  {
    _nestedAction = nestedAction;
  }

  public async Task HandleAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    if (request.Headers["authorization"] == "Bearer " + RequiredToken)
    {
      await _nestedAction.HandleAsync(request, response, cancellationToken);
    }
    else
    {
      await Results.Unauthorized().ExecuteAsync(request.HttpContext);
    }
  }
}