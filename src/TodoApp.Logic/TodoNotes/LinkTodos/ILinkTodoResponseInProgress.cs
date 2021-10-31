using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.TodoNotes.LinkTodos;

public interface ILinkTodoResponseInProgress
{
  Task LinkedSuccessfully(TodoCreatedData todo1, TodoCreatedData todo2, CancellationToken cancellationToken);
}