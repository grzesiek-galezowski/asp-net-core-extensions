using Microsoft.AspNetCore.Http;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;
using TodoApp.Logic.Users;

namespace TodoApp.Http;

public interface IResponseInProgressFactory<T>
{
  T CreateResponseInProgress(HttpResponse response);
}

public class ResponseInProgressFactory : 
  IResponseInProgressFactory<IAddTodoResponseInProgress>,
  IResponseInProgressFactory<ILinkTodoResponseInProgress>, 
  IResponseInProgressFactory<IRegisterUserResponseInProgress>, 
  IResponseInProgressFactory<ILoginUserResponseInProgress>
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

  IRegisterUserResponseInProgress IResponseInProgressFactory<IRegisterUserResponseInProgress>
    .CreateResponseInProgress(HttpResponse response)
  {
    return new RegisterUserResponseInProgress(response);
  }

  public ILoginUserResponseInProgress CreateResponseInProgress(HttpResponse response)
  {
    return new LoginUserResponseInProgress(response);
  }
}