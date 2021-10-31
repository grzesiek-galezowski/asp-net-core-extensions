using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.Users;

namespace TodoApp.Http;

internal class RegisterUserResponseInProgress : IRegisterUserResponseInProgress
{
  private readonly HttpResponse _response;

  public RegisterUserResponseInProgress(HttpResponse response)
  {
    _response = response;
  }

  public async Task SuccessAsync(CancellationToken cancellationToken)
  {
    await Results.Ok().ExecuteAsync(_response.HttpContext);
  }
}