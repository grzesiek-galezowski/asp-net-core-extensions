using TodoApp.Http;
using TodoApp.Logic;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Bootstrap;

public class EndpointsAdapter
{
  public EndpointsAdapter(
    ICommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress> addTodoCommandFactory,
    ICommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress> linkTodoCommandFactory)
  {
    LinkTodoEndpoint = 
      new ExecutingCommandEndpoint<LinkTodosRequestData, ILinkTodoResponseInProgress>(
        new JsonDocumentBasedRequestParser(), 
        linkTodoCommandFactory, 
        new ResponseInProgressFactory());
    
    AddTodoEndpoint = 
      new ExecutingCommandEndpoint<CreateTodoRequestData, IAddTodoResponseInProgress>(
        new JsonDocumentBasedRequestParser(), 
        addTodoCommandFactory, 
        new ResponseInProgressFactory());
  }

  public IAsyncEndpoint LinkTodoEndpoint { get; }
  public IAsyncEndpoint AddTodoEndpoint { get; }
}