using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.Users;

namespace TodoApp.Http;

public class LoginUserResponseInProgress : ILoginUserResponseInProgress
{
  private readonly HttpResponse _response;

  public LoginUserResponseInProgress(HttpResponse response)
  {
    _response = response;
  }

  public async Task SuccessAsync(Guid id, CancellationToken cancellationToken)
  {
    await Results.Ok(id).ExecuteAsync(_response.HttpContext);
  }

  public async Task SorryAsync(CancellationToken cancellationToken)
  {
    await Results.NotFound().ExecuteAsync(_response.HttpContext);
  }
}