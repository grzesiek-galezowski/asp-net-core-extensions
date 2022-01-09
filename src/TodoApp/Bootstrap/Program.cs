using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using TodoApp.Bootstrap;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(ctx => new ServiceLogicRoot(
  ctx.GetTokenValidationParameters(),
  ctx.GetRequiredService<ILoggerFactory>()));

builder.Logging.ClearProviders();
builder.Logging.AddNLog(provider =>
{
  var loggingConfiguration = new LoggingConfiguration();
  var target = new ColoredConsoleTarget("coloredConsole");
  ConfigForLogger.ConfigureAndAddLoggingTarget(target, loggingConfiguration);
  return new LogFactory(loggingConfiguration);
});
builder.Logging.AddNLogWeb();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();

var serviceLogicRoot = app.Services.GetRequiredService<ServiceLogicRoot>();

app.MapPost("/todo",
  async (HttpRequest request, HttpResponse response, CancellationToken token) =>
  {
    await serviceLogicRoot.AddTodoEndpoint.Handle(request, response, token);
  });

app.MapPost("/todo/{id1}/link/{id2}",
  async (HttpRequest request, HttpResponse response, CancellationToken token) =>
  {
    await serviceLogicRoot.LinkTodoEndpoint.Handle(request, response, token);
  });

app.Run();


namespace TodoApp.Bootstrap
{
  public partial class Program
  {
  }
}