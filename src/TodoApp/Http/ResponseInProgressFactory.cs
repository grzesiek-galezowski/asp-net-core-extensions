using Microsoft.AspNetCore.Http;
using TodoApp.Logic;

namespace TodoApp.Http;

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
    return new LinkTodoResponseInProgress(response);
  }
}