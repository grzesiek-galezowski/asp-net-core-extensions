using System.Threading.Tasks;

namespace TodoApp
{
  public class AddTodoCommand
  {
    private readonly TodoDto _dto;
    private readonly IIdGenerator _idGenerator;
    private readonly IAddTodoResponseInProgress _responseInProgress;

    public AddTodoCommand(TodoDto dto, IIdGenerator idGenerator, IAddTodoResponseInProgress addTodoResponseInProgress)
    {
      _idGenerator = idGenerator;
      _responseInProgress = addTodoResponseInProgress;
      _dto = dto;
    }

    public Task ExecuteAsync()
    {
      var id = _idGenerator.Generate();

      return _responseInProgress.SuccessAsync(new TodoCreatedDto
      {
        Content = _dto.Content,
        Title = _dto.Title,
        Id = id
      });
    }
  }
}