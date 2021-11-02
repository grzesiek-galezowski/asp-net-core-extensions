using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http;

public interface IResponseInProgressFactory<T>
{
  T CreateResponseInProgress(HttpResponse response);
}

public class ResponseInProgressFactory : 
  IResponseInProgressFactory<IAddTodoResponseInProgress>,
  IResponseInProgressFactory<ILinkTodoResponseInProgress>
{
  IAddTodoResponseInProgress IResponseInProgressFactory<IAddTodoResponseInProgress>
    .CreateResponseInProgress(HttpResponse response)
  {
    return new AddTodoResponseInProgress(response);
  }

  ILinkTodoResponseInProgress IResponseInProgressFactory<ILinkTodoResponseInProgress>
    .CreateResponseInProgress(HttpResponse response)
  {
    return new LinkTodoResponseInProgress(response);
  }
}