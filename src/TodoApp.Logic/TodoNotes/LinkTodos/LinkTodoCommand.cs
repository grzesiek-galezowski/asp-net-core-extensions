using System;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.TodoNotes.LinkTodos;

public class LinkTodoCommand : IAppCommand
{
  private readonly IUserTodosDao _userTodos;
  private readonly Guid _id1;
  private readonly Guid _id2;
  private readonly ILinkTodoResponseInProgress _responseInProgress;

  public LinkTodoCommand(IUserTodosDao userTodos, Guid id1, Guid id2,
    ILinkTodoResponseInProgress responseInProgress)
  {
    _userTodos = userTodos;
    _id1 = id1;
    _id2 = id2;
    _responseInProgress = responseInProgress;
  }

  public async Task Execute(CancellationToken cancellationToken)
  {
    var todo1 = await _userTodos.Load(_id1, cancellationToken);
    var todo2 = await _userTodos.Load(_id2, cancellationToken);
    todo1 = todo1 with { Links = todo1.Links.Add(todo2.Id)};
    await _responseInProgress.LinkedSuccessfully(todo1, todo2, cancellationToken);
  }
}