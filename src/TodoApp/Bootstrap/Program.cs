using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Bootstrap;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(_ => new ServiceLogicRoot());

var app = builder.Build();

app.MapPost("/todo",
  async ([FromServices] ServiceLogicRoot root, HttpRequest request, HttpResponse response, CancellationToken token) =>
  {
    await root.AddTodoAction().ExecuteAsync(request, response, token);
  });

app.MapPost("/todo/{id1}/link/{id2}",
  async ([FromServices] ServiceLogicRoot root, HttpRequest request, HttpResponse response, CancellationToken token) =>
  {
    await root.LinkTodoAction().ExecuteAsync(request, response, token);
  });

app.Run();


public partial class Program { }