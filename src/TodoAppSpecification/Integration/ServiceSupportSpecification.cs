using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Targets;
using NLog.Web;
using TodoApp.Bootstrap;
using TodoApp.Support;

namespace TodoAppSpecification.Integration;

public class ServiceSupportSpecification
{
  [Test]
  public void ShouldLogAccordingToFormat() //bug better name, cleanup, other messages
  {
    //GIVEN
    var inMemoryLogs = new MemoryTarget("memory");
    using var loggerFactory = new LoggerFactory(new[]
    {
      new NLogLoggerProvider(NLogAspNetCoreOptions.Default,
        ConfigForLogger.CreateLogFactory(inMemoryLogs))
    });
    var support = new ServiceSupport(loggerFactory);
    var customerId = Any.String();
    var requestId = Any.String();
    var operationName = Any.String();

    //WHEN
    using (support.BeginScope(this, new Dictionary<string, object>
           {
             ["customerId"] = customerId,
             ["requestId"] = requestId,
             ["operationName"] = operationName
           }))
    {
      support.AuthorizationFailed(this, new Exception());
    }

    //THEN
    inMemoryLogs.Logs.Should().HaveCount(1);
    inMemoryLogs.Logs.ElementAt(0).Should().Match(
      CurrentDateString() +
      $" *|0|ERROR|{GetType().FullName}|" +
      $"Authorization failed System.Exception: Exception of type 'System.Exception' was thrown." +
      $"|requestId={requestId}" +
      $"|operationName={operationName}" +
      $"|customerId={customerId}");
  }

  private static string CurrentDateString()
  {
    return $"{DateTime.Now.Year.ToString("D2")}-{DateTime.Now.Month.ToString("D2")}-{DateTime.Now.Day.ToString("D2")}";
  }
}