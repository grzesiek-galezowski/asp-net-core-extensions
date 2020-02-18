using System.Threading.Tasks;

namespace TodoApp.App
{
  public class AddTodoCommand : ITodoCommand
  {
    private readonly TodoDto _dto;
    private readonly IIdGenerator _idGenerator;
    private readonly IAddTodoResponseInProgress _responseInProgress;
    private readonly ITodoRepository _todoRepository;

    public AddTodoCommand(
      TodoDto dto, 
      IIdGenerator idGenerator, 
      IAddTodoResponseInProgress addTodoResponseInProgress, 
      ITodoRepository todoRepository)
    {
      _idGenerator = idGenerator;
      _responseInProgress = addTodoResponseInProgress;
      _dto = dto;
      _todoRepository = todoRepository;
    }

    public async Task ExecuteAsync()
    {
      var id = _idGenerator.Generate();

      var todoCreatedDto = new TodoCreatedDto
      {
        Content = _dto.Content,
        Title = _dto.Title,
        Id = id
      };
      
      await _todoRepository.SaveAsync(todoCreatedDto);
      await _responseInProgress.SuccessAsync(todoCreatedDto);
    }
  }
}