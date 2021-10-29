using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.AddTodo;
using TodoApp.Logic.LinkTodos;

namespace TodoApp.Http;

public interface IRequestParser<T>
{
  Task<T> ParseAsync(HttpRequest request, CancellationToken cancellationToken);
}

public class JsonDocumentBasedRequestParser 
  : IRequestParser<CreateTodoRequestData>,
    IRequestParser<LinkTodosRequestData>
{
  async Task<CreateTodoRequestData> IRequestParser<CreateTodoRequestData>.
    ParseAsync(HttpRequest request, CancellationToken cancellationToken)
  {
    using var doc = await JsonDocument.ParseAsync(request.Body, cancellationToken: cancellationToken);
    return new CreateTodoRequestData(doc.Title(), doc.Content());
  }

  Task<LinkTodosRequestData> IRequestParser<LinkTodosRequestData>.ParseAsync(HttpRequest request,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(new LinkTodosRequestData(request.Id1(), request.Id2()));
  }
}