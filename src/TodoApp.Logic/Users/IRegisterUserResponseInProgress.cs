using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.Users;

public interface IRegisterUserResponseInProgress
{
  Task SuccessAsync(CancellationToken cancellationToken);
}