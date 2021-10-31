using System;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Logic.TodoNotes;

public interface IUserTodosDao
{
  Task SaveAsync(Guid id, CreateTodoRequestData todoData, CancellationToken cancellationToken);
  Task<TodoCreatedData> LoadAsync(Guid id, CancellationToken cancellationToken);
}