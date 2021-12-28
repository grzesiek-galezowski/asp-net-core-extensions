using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http;

public class JsonDocumentBasedRequestParser 
  : IRequestParser<CreateTodoRequestData>,
    IRequestParser<LinkTodosRequestData>
{
  private readonly AddTodoDtoParser _addTodoDtoParser;

  public JsonDocumentBasedRequestParser()
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

  async Task<LinkTodosRequestData> IRequestParser<LinkTodosRequestData>.ParseAsync(HttpRequest request,
    CancellationToken cancellationToken)
  {
    return await Task.FromResult(new LinkTodosRequestData(request.Id1(), request.Id2()));
  }
}