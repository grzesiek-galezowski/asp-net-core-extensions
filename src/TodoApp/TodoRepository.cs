using System.Threading.Tasks;
using TodoApp.App;
using TodoApp.Bootstrap;

namespace TodoApp
{
  public class TodoRepository : ITodoRepository
  {
    public async Task SaveAsync(TodoCreatedDto todoCreatedDto)
    {
      await new TodoContext().Todos.AddAsync(todoCreatedDto);
    }
  }
}