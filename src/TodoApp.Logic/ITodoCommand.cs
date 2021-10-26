using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic;

public interface ITodoCommand
{
  Task ExecuteAsync(CancellationToken cancellationToken);
}