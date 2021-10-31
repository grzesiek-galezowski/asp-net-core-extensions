using System;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Logic.Users;

public interface ILoginUserResponseInProgress
{
  Task SuccessAsync(Guid id, CancellationToken cancellationToken);
  Task SorryAsync(CancellationToken cancellationToken);
}