using Microsoft.AspNetCore.Http;
using TodoApp.Bootstrap;
using TodoApp.Logic.App;

namespace TodoApp
{
  public class LinkTodoResponseInProgress : ILinkTodoResponseInProgress
  {
    public LinkTodoResponseInProgress(HttpResponse response)
    {
      throw new System.NotImplementedException();
    }

    public void LinkedSuccessfully(TodoCreatedDto todo1, TodoCreatedDto todo2)
    {
      throw new System.NotImplementedException();
    }
  }
}