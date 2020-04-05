using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public class AddTodoCommand : ITodoCommand
  {
    private readonly TodoDto _dto;
    private readonly IIdGenerator _idGenerator;
    private readonly IAddTodoResponseInProgress _responseInProgress;
    private readonly IUserTodos _userTodos;

    public AddTodoCommand(TodoDto dto,
      IIdGenerator idGenerator,
      IUserTodos userTodos,
      IAddTodoResponseInProgress addTodoResponseInProgress)
    {
      _idGenerator = idGenerator;
      _responseInProgress = addTodoResponseInProgress;
      _dto = dto;
      _userTodos = userTodos;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      var id = _idGenerator.Generate();

      var todoCreatedDto = new TodoCreatedDto
      {
        Content = _dto.Content,
        Title = _dto.Title,
        Id = id
      };
      
      await _userTodos.SaveAsync(todoCreatedDto, cancellationToken);
      await _responseInProgress.SuccessAsync(todoCreatedDto);
    }
  }
}