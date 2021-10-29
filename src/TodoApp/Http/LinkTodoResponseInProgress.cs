using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic;
using TodoApp.Logic.LinkTodos;

namespace TodoApp.Http;

public class LinkTodoResponseInProgress : ILinkTodoResponseInProgress
{
  private readonly HttpResponse _response;

  public LinkTodoResponseInProgress(HttpResponse response)
  {
    _response = response;
  }

  public async Task LinkedSuccessfully(TodoCreatedData todo1, TodoCreatedData todo2,
    CancellationToken cancellationToken)
  {
    await Results.Ok(new
    {
      todo1 = todo1,
      todo2 = todo2,
    }).ExecuteAsync(_response.HttpContext);
  }
}