using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.TodoNotes.AddTodo;

public class AddTodoCommand : IAppCommand
{
  private readonly CreateTodoRequestData _requestData;
  private readonly IIdSequence _idSequence;
  private readonly IAddTodoResponseInProgress _responseInProgress;
  private readonly IUserTodosDao _userTodos;

  public AddTodoCommand(CreateTodoRequestData requestData,
    IIdSequence idSequence,
    IUserTodosDao userTodos,
    IAddTodoResponseInProgress addTodoResponseInProgress)
  {
    _idSequence = idSequence;
    _responseInProgress = addTodoResponseInProgress;
    _requestData = requestData;
    _userTodos = userTodos;
  }

  public async Task Execute(CancellationToken cancellationToken)
  {
    var id = _idSequence.Generate();
    await _userTodos.Save(id, _requestData, cancellationToken);
    await _responseInProgress.Success(id);
  }
}