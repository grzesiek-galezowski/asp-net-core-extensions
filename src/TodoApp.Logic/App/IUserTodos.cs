using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public interface IUserTodos
  {
    Task SaveAsync(TodoCreatedDto todoCreatedDto, CancellationToken cancellationToken);
    Task<TodoCreatedDto> LoadAsync(string? id, CancellationToken cancellationToken);
  }
}