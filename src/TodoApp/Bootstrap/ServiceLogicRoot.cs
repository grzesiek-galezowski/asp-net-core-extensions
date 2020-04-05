using TodoApp.Logic.App;

namespace TodoApp.Bootstrap
{
  internal class ServiceLogicRoot
  {
    private readonly IAsyncAction _addTodoAction;
    private readonly IAsyncAction _linkTodoAction;

    public ServiceLogicRoot()
    {
      var requestParser = new RequestParser();
      var commandFactory = new TodoCommandFactory(new IdGenerator(), new UserTodos());
      var responseInProgressFactory = new ResponseInProgressFactory();
      
      _addTodoAction = new TokenValidationAction(
        new ExecuteCommandAction<TodoDto, IAddTodoResponseInProgress>(
          requestParser, 
          commandFactory, 
          responseInProgressFactory));

      _linkTodoAction = new TokenValidationAction(
        new ExecuteCommandAction<LinkTodoDto, ILinkTodoResponseInProgress>(
          requestParser, 
          commandFactory, 
          responseInProgressFactory));


    }

    public IAsyncAction AddTodoAction()
    {
      return _addTodoAction;
    }

    public IAsyncAction LinkTodoAction()
    {
      return _linkTodoAction;
    }
  }
}