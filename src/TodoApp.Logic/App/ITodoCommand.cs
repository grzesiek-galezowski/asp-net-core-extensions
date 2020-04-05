using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public interface ITodoCommand
  {
    Task ExecuteAsync(CancellationToken cancellationToken);
  }
}