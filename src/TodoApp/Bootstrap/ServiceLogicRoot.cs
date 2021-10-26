using TodoApp.Db;
using TodoApp.Http;
using TodoApp.Logic;
using TodoApp.Random;

namespace TodoApp.Bootstrap;

public class ServiceLogicRoot
{
  private readonly IAsyncEndpoint _addTodoAction;
  private readonly IAsyncEndpoint _linkTodoAction;

  public ServiceLogicRoot()
  {
    var requestParser = new JsonDocumentBasedRequestParser();
    var commandFactory = new TodoCommandFactory(new IdGenerator(), new UserTodosDao());
    var responseInProgressFactory = new ResponseInProgressFactory();
      
    _addTodoAction = new TokenValidatingEndpoint(
      new ExecutingCommandEndpoint<CreateTodoRequestData, IAddTodoResponseInProgress>(
        requestParser, 
        commandFactory, 
        responseInProgressFactory));

    _linkTodoAction = new TokenValidatingEndpoint(
      new ExecutingCommandEndpoint<LinkTodosRequestData, ILinkTodoResponseInProgress>(
        requestParser, 
        commandFactory, 
        responseInProgressFactory));


  }

  public IAsyncEndpoint AddTodoAction()
  {
    return _addTodoAction;
  }

  public IAsyncEndpoint LinkTodoAction()
  {
    return _linkTodoAction;
  }
}