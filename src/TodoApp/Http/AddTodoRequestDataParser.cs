using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Http;

public class AddTodoRequestDataParser : IRequestParser<CreateTodoRequestData>
{
  private readonly IJsonElementParser<AddTodoDto> _addTodoDtoParser;

  public AddTodoRequestDataParser(IJsonElementParser<AddTodoDto> addTodoDtoParser)
  {
    _addTodoDtoParser = addTodoDtoParser;
  }

  //bug get back to this parser when second request with body is added
  // and make it more generic, e.g. body parser or sth.
  public async Task<CreateTodoRequestData> ParseAsync(HttpRequest request, CancellationToken cancellationToken)
  {
    using var doc = await JsonDocument.ParseAsync(request.Body, cancellationToken: cancellationToken);

    var ((title, content), _) = _addTodoDtoParser.Parse(doc.RootElement);
    return new CreateTodoRequestData(title, content);
  }
}