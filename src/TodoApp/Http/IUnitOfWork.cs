using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Http;

public interface IUnitOfWork
{
  Task SaveChangesAsync(CancellationToken cancellationToken);
}