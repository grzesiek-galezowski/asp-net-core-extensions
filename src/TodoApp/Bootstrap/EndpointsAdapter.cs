using TodoApp.Http;
using TodoApp.Logic;
using TodoApp.Logic.AddTodo;
using TodoApp.Logic.LinkTodos;

namespace TodoApp.Bootstrap;

public class EndpointsAdapter
{
  public EndpointsAdapter(
    ITodoCommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress> addTodoCommandFactory,
    ITodoCommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress> linkTodoCommandFactory
  )
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
  }

  public IAsyncEndpoint LinkTodoEndpoint { get; }
  public IAsyncEndpoint AddTodoEndpoint { get; }
}