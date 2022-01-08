using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Http.AddTodo;

public class AddTodoResponseInProgress : IAddTodoResponseInProgress
{
  private readonly HttpResponse _response;

  public AddTodoResponseInProgress(HttpResponse response)
  {
    _response = response;
  }

  public async Task Success(Guid id)
  {
    var result = Results.Ok(id);
    await result.ExecuteAsync(_response.HttpContext);
    Console.WriteLine(_response.HttpContext);
  }
}