using System.Threading.Tasks;

namespace TodoApp.App
{
  public interface ITodoRepository
  {
    Task SaveAsync(TodoCreatedDto todoCreatedDto);
  }
}