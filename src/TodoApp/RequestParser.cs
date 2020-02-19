using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Bootstrap;
using TodoApp.Logic.App;

namespace TodoApp
{
  public interface IRequestParser<T>
  {
    Task<T> ParseAsync(HttpRequest request);
  }

  public class RequestParser 
    : IRequestParser<TodoDto>,
      IRequestParser<LinkTodoDto>
  {
    public Task<TodoDto> ParseAsync(HttpRequest request)
    {
      return JsonSerializer.DeserializeAsync<TodoDto>(request.Body).AsTask();
    }

    async Task<LinkTodoDto> IRequestParser<LinkTodoDto>.ParseAsync(HttpRequest request)
    {
      return new LinkTodoDto()
      {
        Id1 = request.RouteValues["id1"].ToString(),
        Id2 = request.RouteValues["id2"].ToString(),
      };
    }
  }
}