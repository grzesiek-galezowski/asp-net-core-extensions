using Microsoft.AspNetCore.Http;
using TodoApp.Logic;

namespace TodoApp.Http;

public class LinkTodoResponseInProgress : ILinkTodoResponseInProgress
{
  public LinkTodoResponseInProgress(HttpResponse response)
  {
    throw new System.NotImplementedException();
  }

  public void LinkedSuccessfully(TodoCreatedData todo1, TodoCreatedData todo2)
  {
    throw new System.NotImplementedException();
  }
}