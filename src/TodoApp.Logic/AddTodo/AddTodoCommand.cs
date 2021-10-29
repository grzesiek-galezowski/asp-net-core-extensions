using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.AddTodo;

public class AddTodoCommand : ITodoCommand
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

    var todoCreatedDto = new TodoCreatedData(
      Title: _requestData.Title, 
      Content: _requestData.Content, 
      Links: ImmutableHashSet<Guid>.Empty,
      Id: id);

      
    await _userTodos.SaveAsync(todoCreatedDto, cancellationToken);
    await _responseInProgress.SuccessAsync(todoCreatedDto);
  }
}