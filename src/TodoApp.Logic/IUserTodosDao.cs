using System;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic;

public interface IUserTodosDao
{
  Task SaveAsync(TodoCreatedData todoData, CancellationToken cancellationToken);
  Task<TodoCreatedData> LoadAsync(Guid id, CancellationToken cancellationToken);
}