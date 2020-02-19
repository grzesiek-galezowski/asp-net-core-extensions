using System.Threading.Tasks;
using FluentAssertions;
using TodoApp.Bootstrap;
using TodoApp.Logic.App;

namespace TodoApp
{
  public class TodoRepository : ITodoRepository
  {
    public async Task SaveAsync(TodoCreatedDto todoCreatedDto)
    {
      await new TodoContext().Todos.AddAsync(todoCreatedDto);
    }

    public Task<TodoCreatedDto> ReadAsync(string? id)
    {
      return new TodoContext().Todos.FindAsync(id).AsTask();
    }
  }
}