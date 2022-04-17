using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace TodoApp.Support;

public static class LoggingAdapter
{

  public static LogFactory CreateConsoleLogFactory()
  {
    return CreateLogFactory(new ColoredConsoleTarget("coloredConsole"));
  }

  public static LogFactory CreateLogFactory(TargetWithLayout target)
  {
    var loggingConfiguration = new LoggingConfiguration();
    ConfigForLogger.ConfigureAndAddLoggingTarget(
      target,
      loggingConfiguration);
    return new LogFactory(loggingConfiguration);
  }

  public static ServiceSupport CreateServiceSupport(ILoggerFactory loggerFactory)
  {
    return new ServiceSupport(loggerFactory);
  }
}