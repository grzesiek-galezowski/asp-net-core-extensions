using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using TodoApp.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

var webAppInitialization = new Lazy<WebApplication>(() => builder.Build());

builder.Services.AddSingleton(ctx => new ServiceLogicRoot(
  ctx.GetTokenValidationParameters(),
  ctx.GetRequiredService<ILoggerFactory>()));

builder.SetupNLog();
builder.Services.AddHealthChecks().AddAsyncCheck("ready", 
  async cancellationToken => 
    await webAppInitialization.Value
      .Services.GetRequiredService<ServiceLogicRoot>()
      .IsReadyHealthCheck
      .RetrieveStatus(cancellationToken));

var app = webAppInitialization.Value;
var serviceLogicRoot = app.Services.GetRequiredService<ServiceLogicRoot>();

app.MapHealthChecks("health");
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