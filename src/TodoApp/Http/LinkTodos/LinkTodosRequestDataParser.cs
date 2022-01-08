using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.ParsingJson;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http.LinkTodos;

public class LinkTodosRequestDataParser : IRequestParser<LinkTodosRequestData>
{
  public async Task<LinkTodosRequestData> ParseAsync(HttpRequest request,
    CancellationToken cancellationToken)
  {
    return await Task.FromResult(new LinkTodosRequestData(request.Id1(), request.Id2()));
  }
}