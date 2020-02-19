using TodoApp.Bootstrap;
using TodoApp.Logic.App;

namespace TodoApp
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
    private readonly ITodoRepository _todoRepository;

    public TodoCommandFactory(IIdGenerator idGenerator, ITodoRepository todoRepository)
    {
      _idGenerator = idGenerator;
      _todoRepository = todoRepository;
    }

    public ITodoCommand CreateCommand(TodoDto dto, IAddTodoResponseInProgress responseInProgress)
    {
      return new AddTodoCommand(dto, _idGenerator, responseInProgress, _todoRepository);
    }

    public ITodoCommand CreateCommand(LinkTodoDto dto, ILinkTodoResponseInProgress responseInProgress)
    {
      throw new System.NotImplementedException();
    }
  }
}