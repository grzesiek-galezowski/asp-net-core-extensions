using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.App;

namespace TodoApp
{
  public class AddTodoResponseInProgress : IAddTodoResponseInProgress
  {
    private readonly HttpResponse _response;

    public AddTodoResponseInProgress(HttpResponse response)
    {
      _response = response;
    }

    public Task SuccessAsync(TodoCreatedDto todoCreatedDto)
    {
      return JsonSerializer.SerializeAsync(_response.Body,
        todoCreatedDto);
    }
  }
}