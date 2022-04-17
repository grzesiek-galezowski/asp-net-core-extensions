using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Targets;
using NLog.Web;
using TodoApp.Support;

namespace TodoAppSpecification.Integration;

public class ServiceSupportDriver : IDisposable
{
  private readonly MemoryTarget _inMemoryLogs;
  private readonly ServiceSupport _support;
  private static LoggerFactory _loggerFactory;

  public ServiceSupportDriver()
  {
    _inMemoryLogs = new MemoryTarget("memory");
    _loggerFactory = new LoggerFactory(new[]
    {
      new NLogLoggerProvider(NLogAspNetCoreOptions.Default,
        LoggingAdapter.CreateLogFactory(_inMemoryLogs))
    });
    _support = LoggingAdapter.CreateServiceSupport(_loggerFactory);
  }

  public void Dispose()
  {
    _loggerFactory.Dispose();
    _inMemoryLogs.Dispose();
  }

  public IDisposable BeginScope(string customerId, string requestId, string operationName)
  {
    return _support.BeginScope(this, new Dictionary<string, object>
    {
      ["customerId"] = customerId,
      ["requestId"] = requestId,
      ["operationName"] = operationName
    });
  }

  public void NotifyAuthorizationFailed(Exception exception)
  {
    _support.AuthorizationFailed(this, exception);
  }

  public void LogCountShouldBe(int expected)
  {
    _inMemoryLogs.Logs.Should().HaveCount(expected);
  }

  public void FirstLogShouldMatch(string firstLog)
  {
    _inMemoryLogs.Logs.ElementAt(0).Should().Match(firstLog);
  }
}