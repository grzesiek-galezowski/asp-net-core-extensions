using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Bootstrap;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddTodoAppSpecificJwtAuthenticationConfig();
builder.Services.AddSingleton(ctx => new ServiceLogicRoot());

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/todo",
  async ([FromServices] ServiceLogicRoot root, HttpRequest request, HttpResponse response, CancellationToken token) =>
  {
    await root.AddTodoEndpoint.HandleAsync(request, response, token);
  }).RequireAuthorization();

app.MapPost("/todo/{id1}/link/{id2}",
  async ([FromServices] ServiceLogicRoot root, HttpRequest request, HttpResponse response, CancellationToken token) =>
  {
    await root.LinkTodoEndpoint.HandleAsync(request, response, token);
  });

app.Run();


namespace TodoApp.Bootstrap
{
  public partial class Program { }
}