using System;
using System.Threading.Tasks;

namespace TodoApp.Logic.TodoNotes.AddTodo;

public interface IAddTodoResponseInProgress
{
  Task SuccessAsync(Guid id);
}