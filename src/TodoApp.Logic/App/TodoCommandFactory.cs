using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public interface ITodoCommandFactory<in TDto, in TResponse>
  {
    ITodoCommand CreateCommand(TDto dto, TResponse responseInProgress);
  }

  public class TodoCommandFactory : 
    ITodoCommandFactory<TodoDto, IAddTodoResponseInProgress>,
    ITodoCommandFactory<LinkTodoDto, ILinkTodoResponseInProgress>
  {
    private readonly IIdGenerator _idGenerator;
    private readonly IUserTodos _userTodos;

    public TodoCommandFactory(IIdGenerator idGenerator, IUserTodos userTodos)
    {
      _idGenerator = idGenerator;
      _userTodos = userTodos;
    }

    public ITodoCommand CreateCommand(TodoDto dto, IAddTodoResponseInProgress responseInProgress)
    {
      return new AddTodoCommand(dto, _idGenerator, _userTodos, responseInProgress);
    }

    public ITodoCommand CreateCommand(LinkTodoDto dto, ILinkTodoResponseInProgress responseInProgress)
    {
      return new LinkTodoCommand(_userTodos, dto.Id1, dto.Id2, responseInProgress);
    }
  }
}