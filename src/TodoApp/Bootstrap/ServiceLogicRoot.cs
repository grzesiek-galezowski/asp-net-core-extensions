using TodoApp.App;

namespace TodoApp.Bootstrap
{
  internal class ServiceLogicRoot
  {
    private readonly TokenValidationAction _addTodoAction;

    public ServiceLogicRoot()
    {
      _addTodoAction = new TokenValidationAction(
        new AddTodoAction<TodoDto, IAddTodoResponseInProgress>(
          new RequestParser<TodoDto>(), 
          new TodoCommandFactory(new IdGenerator(), new TodoRepository()), 
          new ResponseInProgressFactory()));
    }

    public IAsyncAction AddTodoAction()
    {
      return _addTodoAction;
    }
  }
}