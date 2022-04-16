using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace TodoApp.Support;

public static class ConfigForLogger
{
  public static void ConfigureAndAddLoggingTarget(TargetWithLayout target, LoggingConfiguration loggingConfiguration)
  {
    //bug integration tests of support should be possible now
    target.Layout =
      Layout.FromString( //bug FromMethod allows even more customization
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

  public static LogFactory CreateLogFactory(TargetWithLayout inMemoryLogs)
  {
    var loggingConfiguration = new LoggingConfiguration();
    ConfigureAndAddLoggingTarget(
      inMemoryLogs,
      loggingConfiguration);
    return new LogFactory(loggingConfiguration);
  }
}