using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.TodoNotes.AddTodo;

public class AddTodoCommand : IAppCommand
{
  private readonly CreateTodoRequestData _requestData;
  private readonly IIdGenerator _idGenerator;
  private readonly IAddTodoResponseInProgress _responseInProgress;
  private readonly IUserTodosDao _userTodos;

  public AddTodoCommand(CreateTodoRequestData requestData,
    IIdGenerator idGenerator,
    IUserTodosDao userTodos,
    IAddTodoResponseInProgress addTodoResponseInProgress)
  {
    _idGenerator = idGenerator;
    _responseInProgress = addTodoResponseInProgress;
    _requestData = requestData;
    _userTodos = userTodos;
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    var id = _idGenerator.Generate();
    await _userTodos.SaveAsync(id, _requestData, cancellationToken);
    await _responseInProgress.SuccessAsync(id);
  }
}