using System.Threading;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http.LinkTodos;

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