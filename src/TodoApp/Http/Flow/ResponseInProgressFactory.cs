using Microsoft.AspNetCore.Http;
using TodoApp.Http.AddTodo;
using TodoApp.Http.LinkTodos;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http.Flow;

public interface IResponseInProgressFactory<out T>
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