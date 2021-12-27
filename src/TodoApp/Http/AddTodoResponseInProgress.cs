using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Http;

public class AddTodoResponseInProgress : IAddTodoResponseInProgress
{
  private readonly HttpResponse _response;

  public AddTodoResponseInProgress(HttpResponse response)
  {
    _response = response;
  }

  public async Task SuccessAsync(Guid id)
  {
    var result = Results.Ok(id);
    await result.ExecuteAsync(_response.HttpContext);
    Console.WriteLine(_response.HttpContext);
  }
}