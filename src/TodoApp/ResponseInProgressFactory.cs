using Microsoft.AspNetCore.Http;
using TodoApp.Bootstrap;
using TodoApp.Logic.App;

namespace TodoApp
{
  public interface IResponseInProgressFactory<T>
  {
    T CreateResponseInProgress(HttpResponse response);
  }

  public class ResponseInProgressFactory : 
    IResponseInProgressFactory<IAddTodoResponseInProgress>,
    IResponseInProgressFactory<ILinkTodoResponseInProgress>
  {
    public IAddTodoResponseInProgress CreateResponseInProgress(HttpResponse response)
    {
      return new AddTodoResponseInProgress(response);
    }

    ILinkTodoResponseInProgress IResponseInProgressFactory<ILinkTodoResponseInProgress>.CreateResponseInProgress(HttpResponse response)
    {
      throw new System.NotImplementedException();
    }
  }
}