using System.Net;
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

    public Task ExecuteAsync(HttpRequest request, HttpResponse response)
    {
      if (request.Headers["authorization"] == "Bearer " + RequiredToken)
      {
        return _nestedAction.ExecuteAsync(request, response);
      }
      else
      {
        response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return Task.CompletedTask;
      }
    }
  }
}