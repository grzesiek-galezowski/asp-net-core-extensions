using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic;

namespace TodoApp.Http;

public class AddTodoResponseInProgress : IAddTodoResponseInProgress
{
  private readonly HttpResponse _response;

  public AddTodoResponseInProgress(HttpResponse response)
  {
    _response = response;
  }

  public async Task SuccessAsync(TodoCreatedData todoCreatedData)
  {
    var result = Results.Ok(todoCreatedData);
    await result.ExecuteAsync(_response.HttpContext);
  }
}