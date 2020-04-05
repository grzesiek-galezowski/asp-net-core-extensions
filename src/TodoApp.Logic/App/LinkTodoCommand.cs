using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public class LinkTodoCommand : ITodoCommand
  {
    private readonly IUserTodos _userTodos;
    private readonly string? _id1;
    private readonly string? _id2;
    private readonly ILinkTodoResponseInProgress _responseInProgress;

    public LinkTodoCommand(IUserTodos userTodos, string? id1, string? id2,
      ILinkTodoResponseInProgress responseInProgress)
    {
      _userTodos = userTodos;
      _id1 = id1;
      _id2 = id2;
      _responseInProgress = responseInProgress;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      var todo1 = await _userTodos.LoadAsync(_id1, cancellationToken);
      var todo2 = await _userTodos.LoadAsync(_id2, cancellationToken);
      todo1.Links.Add(todo2.Id);
      await _userTodos.SaveAsync(todo1, cancellationToken);
      _responseInProgress.LinkedSuccessfully(todo1, todo2);
    }
  }
}