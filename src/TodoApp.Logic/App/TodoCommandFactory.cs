using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public interface ITodoCommandFactory<in TDto, in TResponse>
  {
    Task<ITodoCommand> CreateCommandAsync(TDto dto, TResponse responseInProgress);
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

    public async Task<ITodoCommand> CreateCommandAsync(TodoDto dto, IAddTodoResponseInProgress responseInProgress)
    {
      return new AddTodoCommand(dto, _idGenerator, responseInProgress, _todoRepository);
    }

    public async Task<ITodoCommand> CreateCommandAsync(LinkTodoDto dto, ILinkTodoResponseInProgress responseInProgress)
    {
      return new LinkTodoCommand(await _todoRepository.ReadAsync(dto.Id1), await _todoRepository.ReadAsync(dto.Id2));
    }
  }
}