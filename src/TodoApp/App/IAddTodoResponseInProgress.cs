using System.Threading.Tasks;

namespace TodoApp.App
{
  public interface IAddTodoResponseInProgress
  {
    Task SuccessAsync(TodoCreatedDto todoCreatedDto);
  }
}