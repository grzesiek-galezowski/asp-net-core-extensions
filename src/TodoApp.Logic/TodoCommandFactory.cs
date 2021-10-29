using TodoApp.Logic.AddTodo;
using TodoApp.Logic.LinkTodos;

namespace TodoApp.Logic;

public interface ITodoCommandFactory<in TDto, in TResponse>
{
  ITodoCommand CreateCommand(TDto dto, TResponse responseInProgress);
}

public class TodoCommandFactory : 
  ITodoCommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress>,
  ITodoCommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress>
{
  private readonly IIdGenerator _idGenerator;
  private readonly IUserTodosDao _userTodos;

  public TodoCommandFactory(IIdGenerator idGenerator, IUserTodosDao userTodos)
  {
    _idGenerator = idGenerator;
    _userTodos = userTodos;
  }

  public ITodoCommand CreateCommand(CreateTodoRequestData requestData, IAddTodoResponseInProgress responseInProgress)
  {
    return new AddTodoCommand(requestData, _idGenerator, _userTodos, responseInProgress);
  }

  public ITodoCommand CreateCommand(LinkTodosRequestData data, ILinkTodoResponseInProgress responseInProgress)
  {
    return new LinkTodoCommand(_userTodos, data.Id1, data.Id2, responseInProgress);
  }
}