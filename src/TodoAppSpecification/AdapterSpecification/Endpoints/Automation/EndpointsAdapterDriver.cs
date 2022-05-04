using System.Net.Mime;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using TddXt.HttpContextMock;
using TodoApp.Http;
using TodoApp.Http.Support;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoAppSpecification.AdapterSpecification.Endpoints.Automation;

public class EndpointsAdapterDriver
{
  private readonly EndpointsAdapter _adapter;

  public EndpointsAdapterDriver()
  {
    _adapter = new EndpointsAdapter(
      Substitute.For<ICommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress>>(),
      Substitute.For<ICommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress>>(),
      new TokenValidationParameters
      {
        ValidIssuer = TestTokens.Issuer,
        IssuerSigningKey = TestTokens.SecurityKey
      },
      Substitute.For<IEndpointsSupport>());
  }


  public async Task<AddTodoItemAdapterResponse> AttemptToAddTodoItem()
  {
    return await AttemptToAddTodoItem(_ => _);
  }

  public async Task<AddTodoItemAdapterResponse> AttemptToAddTodoItem(Func<HttpRequestMock, HttpRequestMock> customize)
  {
    var httpContextMock = HttpContextMock.Default();
    await _adapter.AddTodoEndpoint.Handle(
      customize(httpContextMock.Request()
        .AppendPathSegment("todo")
        .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
        .WithHeader("Content-Type", MediaTypeNames.Application.Json)
        .WithHeader("Accept", MediaTypeNames.Application.Json)
        .WithQueryParam("customerId", Any.String())
        .WithJsonBody(new { title = "Meeting", content = "there's a meeting you need to attend" })).RealInstance,
      httpContextMock.Response().RealInstance,
      new CancellationToken()
    );
    var httpResponseMock = httpContextMock.Response();
    return new AddTodoItemAdapterResponse(httpResponseMock);
  }
}