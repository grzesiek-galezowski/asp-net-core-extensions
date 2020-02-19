using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public interface IAddTodoResponseInProgress
  {
    Task SuccessAsync(TodoCreatedDto todoCreatedDto);
  }
}