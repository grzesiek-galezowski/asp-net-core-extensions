using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace TodoApp.Support;

public static class LoggingAdapter
{
  public static LogFactory CreateLogFactory(TargetWithLayout inMemoryLogs)
  {
    var loggingConfiguration = new LoggingConfiguration();
    ConfigureAndAddLoggingTarget(inMemoryLogs, loggingConfiguration);
    return new LogFactory(loggingConfiguration);
  }

  public static LogFactory CreateConsoleLogFactory()
  {
    return CreateLogFactory(new ColoredConsoleTarget("coloredConsole"));
  }

  private static void ConfigureAndAddLoggingTarget(TargetWithLayout target, LoggingConfiguration loggingConfiguration)
  {
    target.Layout = 
      Layout.FromString( //bug FromMethod allows even more customization or maybe JsonLayout
        "${longdate}" +
        "|${event-properties:item=EventId_Id:whenEmpty=0}" +
        "|${uppercase:${level}}" +
        "|${logger}" +
        "|${message} ${exception:format=tostring}" +
        "|requestId=${mdlc:item=requestId}" +
        "|operationName=${mdlc:item=operationName}" +
        "|customerId=${mdlc:item=customerId}");
    loggingConfiguration.AddRuleForAllLevels(target);
  }

  public static ServiceSupport CreateServiceSupport(ILoggerFactory loggerFactory)
  {
    return new ServiceSupport(loggerFactory);
  }
}