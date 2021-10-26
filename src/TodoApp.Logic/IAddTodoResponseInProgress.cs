using System.Threading.Tasks;

namespace TodoApp.Logic;

public interface IAddTodoResponseInProgress
{
  Task SuccessAsync(TodoCreatedData todoCreatedData);
}