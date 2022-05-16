using System.Threading;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TodoApp.Http.HealthChecks;

public class TrivialHealthCheck : IAppHealthCheck
{
  public async Task<HealthCheckResult> RetrieveStatus(CancellationToken cancellationToken)
  {
    //bug make this more complex
    return HealthCheckResult.Healthy();
  }
}