using System.Threading.Tasks;

namespace TodoApp.Logic.AddTodo;

public interface IAddTodoResponseInProgress
{
  Task SuccessAsync(TodoCreatedData todoCreatedData);
}