using Microsoft.AspNetCore.Http;

namespace TodoApp
{
  public interface IResponseInProgressFactory<T>
  {
    T CreateResponseInProgress(HttpResponse response);
  }

  public class ResponseInProgressFactory : IResponseInProgressFactory<IAddTodoResponseInProgress>
  {
    public IAddTodoResponseInProgress CreateResponseInProgress(HttpResponse response)
    {
      return new AddTodoResponseInProgress(response);
    }
  }
}