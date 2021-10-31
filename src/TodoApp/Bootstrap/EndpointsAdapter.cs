using TodoApp.Http;
using TodoApp.Logic;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;
using TodoApp.Logic.Users;

namespace TodoApp.Bootstrap;

public class EndpointsAdapter
{
  public EndpointsAdapter(
    ICommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress> addTodoCommandFactory,
    ICommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress> linkTodoCommandFactory,
    ICommandFactory<RegisterUserRequestData, IRegisterUserResponseInProgress> registerUserCommandFactory,
    ICommandFactory<LoginUserRequestData, ILoginUserResponseInProgress> loginUserCommandFactory)
  {
    LinkTodoEndpoint = new TokenValidatingEndpoint(
      new ExecutingCommandEndpoint<LinkTodosRequestData, ILinkTodoResponseInProgress>(
        new JsonDocumentBasedRequestParser(), 
        linkTodoCommandFactory, 
        new ResponseInProgressFactory()));
    
    AddTodoEndpoint = new TokenValidatingEndpoint(
      new ExecutingCommandEndpoint<CreateTodoRequestData, IAddTodoResponseInProgress>(
        new JsonDocumentBasedRequestParser(), 
        addTodoCommandFactory, 
        new ResponseInProgressFactory()));

    RegisterUserEndpoint = 
      new ExecutingCommandEndpoint<RegisterUserRequestData, IRegisterUserResponseInProgress>(
        new JsonDocumentBasedRequestParser(), 
        registerUserCommandFactory, 
        new ResponseInProgressFactory());

    LoginUserEndpoint = 
      new ExecutingCommandEndpoint<LoginUserRequestData, ILoginUserResponseInProgress>(
        new JsonDocumentBasedRequestParser(), 
        loginUserCommandFactory, 
        new ResponseInProgressFactory());
  }

  public IAsyncEndpoint LinkTodoEndpoint { get; }
  public IAsyncEndpoint AddTodoEndpoint { get; }
  public IAsyncEndpoint RegisterUserEndpoint { get; }
  public IAsyncEndpoint LoginUserEndpoint { get; }
}