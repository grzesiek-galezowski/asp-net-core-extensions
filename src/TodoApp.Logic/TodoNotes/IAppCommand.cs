using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.TodoNotes;

public interface IAppCommand
{
  Task Execute(CancellationToken cancellationToken);
}