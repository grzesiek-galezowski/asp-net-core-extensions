using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TodoApp.Bootstrap;
using TodoApp.Logic.App;

namespace TodoApp
{
  public class UserTodos : IUserTodos
  {
    public Task SaveAsync(TodoCreatedDto todoCreatedDto, CancellationToken cancellationToken)
    {
      return new TodoContext().Todos.AddAsync(todoCreatedDto, cancellationToken).AsTask();
    }

    public Task<TodoCreatedDto> LoadAsync(string? id, CancellationToken cancellationToken)
    {
      return new TodoContext().Todos.FindAsync(id, cancellationToken).AsTask();
    }
  }
}