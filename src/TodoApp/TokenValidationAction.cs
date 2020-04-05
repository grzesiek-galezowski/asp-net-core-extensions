using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
  public class TokenValidationAction : IAsyncAction
  {
    public const string RequiredToken = "trolololo";
    
    private readonly IAsyncAction _nestedAction;

    public TokenValidationAction(IAsyncAction nestedAction)
    {
      _nestedAction = nestedAction;
    }

    public Task ExecuteAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
    {
      if (request.Headers["authorization"] == "Bearer " + RequiredToken)
      {
        return _nestedAction.ExecuteAsync(request, response, cancellationToken);
      }
      else
      {
        response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return Task.CompletedTask;
      }
    }
  }
}