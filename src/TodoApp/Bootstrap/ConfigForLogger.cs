using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace TodoApp.Bootstrap;

public static class ConfigForLogger
{
  public static void ConfigureAndAddLoggingTarget(TargetWithLayout coloredConsoleTarget, LoggingConfiguration loggingConfiguration)
  {
    //bug integration tests of support should be possible now
    coloredConsoleTarget.Layout =
      Layout.FromString( //bug FromMethod allows even more customization
        "${longdate}" +
        "|${event-properties:item=EventId_Id:whenEmpty=0}" +
        "|${uppercase:${level}}" +
        "|${logger}" +
        "|${message} ${exception:format=tostring}" +
        "|requestId=${mdlc:item=requestId}" +
        "|customerId=${mdlc:item=customerId}");
    loggingConfiguration.AddRuleForAllLevels(coloredConsoleTarget);
  }

}