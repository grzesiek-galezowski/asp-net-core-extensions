namespace TodoApp
{
  internal class ServiceLogicRoot
  {
    private readonly TokenValidationAction _addTodoAction;

    public ServiceLogicRoot()
    {
      IIdGenerator idGenerator = new IdGenerator();
      _addTodoAction = new TokenValidationAction(
        new AddTodoAction<TodoDto, IAddTodoResponseInProgress>(
          new RequestParser<TodoDto>(), 
          new TodoCommandFactory(idGenerator), 
          new ResponseInProgressFactory()));
    }

    public IAsyncAction AddTodoAction()
    {
      return _addTodoAction;
    }
  }
}