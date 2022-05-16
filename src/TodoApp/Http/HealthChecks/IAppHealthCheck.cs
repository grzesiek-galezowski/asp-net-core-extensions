using System.Threading;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TodoApp.Http.HealthChecks;

public interface IAppHealthCheck //bug
{
  Task<HealthCheckResult> RetrieveStatus(CancellationToken cancellationToken);
}