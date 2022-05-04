using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApp.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

builder.SetupNLog();

var app = builder.Build();

var serviceLogicRoot = new ServiceLogicRoot(
    app.Services.GetTokenValidationParameters(),
    app.Services.GetRequiredService<ILoggerFactory>()
);

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