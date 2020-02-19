using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public interface ITodoRepository
  {
    Task SaveAsync(TodoCreatedDto todoCreatedDto);
  }
}