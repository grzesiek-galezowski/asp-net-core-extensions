using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
  public class AddTodoAction
  {
    private readonly HttpRequest _request;
    private readonly HttpResponse _response;
    private readonly IIdGenerator _idGenerator;

    public AddTodoAction(HttpRequest request, HttpResponse response, IIdGenerator idGenerator)
    {
      _request = request;
      _response = response;
      _idGenerator = idGenerator;
    }

    public async Task ExecuteAsync()
    {
      var dto = await JsonSerializer.DeserializeAsync<TodoDto>(_request.Body);
      var id = _idGenerator.Generate();

      await JsonSerializer.SerializeAsync(_response.Body, new TodoCreatedDto()
      {
        Content = dto.Content,
        Title = dto.Title,
        Id = id
      });
    }
  }
}