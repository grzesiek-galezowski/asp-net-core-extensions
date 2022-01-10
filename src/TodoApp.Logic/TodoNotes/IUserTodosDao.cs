using System;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Logic.TodoNotes;

public interface IUserTodosDao
{
  Task Save(Guid id, CreateTodoRequestData todoData, CancellationToken cancellationToken);
  Task<TodoCreatedData> Load(Guid id, CancellationToken cancellationToken);
}