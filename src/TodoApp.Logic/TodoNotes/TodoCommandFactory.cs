using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Logic.TodoNotes;

public interface ICommandFactory<in TDto, in TResponse>
{
  IAppCommand CreateCommand(TDto dto, TResponse responseInProgress);
}

public class TodoCommandFactory : 
  ICommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress>,
  ICommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress>
{
  private readonly IIdSequence _idSequence;
  private readonly IUserTodosDao _userTodos;

  public TodoCommandFactory(IIdSequence idSequence, IUserTodosDao userTodos)
  {
    _idSequence = idSequence;
    _userTodos = userTodos;
  }

  public IAppCommand CreateCommand(CreateTodoRequestData requestData, IAddTodoResponseInProgress responseInProgress)
  {
    return new AddTodoCommand(requestData, _idSequence, _userTodos, responseInProgress);
  }

  public IAppCommand CreateCommand(LinkTodosRequestData data, ILinkTodoResponseInProgress responseInProgress)
  {
    return new LinkTodoCommand(_userTodos, data.Id1, data.Id2, responseInProgress);
  }
}