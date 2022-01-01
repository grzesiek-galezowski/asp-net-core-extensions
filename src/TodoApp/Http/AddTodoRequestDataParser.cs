using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Http;

public class AddTodoRequestDataParser : IRequestParser<CreateTodoRequestData>
{
  private readonly AddTodoDtoParser _addTodoDtoParser;

  public AddTodoRequestDataParser()
  {
    _addTodoDtoParser = new AddTodoDtoParser(
      new AddTodoDataParser(
        new RequiredStringParser(nameof(AddTodoDataDto.Title)),
        new RequiredStringParser(nameof(AddTodoDataDto.Content))
      ),
      new DictionaryParser(nameof(AddTodoDto.Links))
    );
  }

  async Task<CreateTodoRequestData> IRequestParser<CreateTodoRequestData>.
    ParseAsync(HttpRequest request, CancellationToken cancellationToken)
  {
    using var doc = await JsonDocument.ParseAsync(request.Body, cancellationToken: cancellationToken);

    var ((title, content), _) = _addTodoDtoParser.Parse(doc);
    return new CreateTodoRequestData(title, content);
  }
}