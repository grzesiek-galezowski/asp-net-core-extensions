using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Flurl.Http;
using TodoApp.Http;
using TodoApp.Http.AddTodo;

namespace TodoAppSpecification.HostSpec;

public class ManagingTodosSpecification
{
  [Test]
  public async Task ShouldBeAbleToCreateATodo()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateToken())
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .AllowAnyHttpStatus()
      .PostJsonAsync(new AddTodoDto(
        new AddTodoDataDto("Meeting", "there's a meeting you need to attend"),
        new Dictionary<string, string>()));

    response.StatusCode.Should().Be(200);
  }

  [Test]
  public async Task ShouldBeAbleToLinkTodos()
  {
    var customerId = Any.String();
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateToken())
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .PostJsonAsync(new AddTodoDto(
        new AddTodoDataDto("Meeting", "there's a meeting you need to attend"),
        new Dictionary<string, string>()));

    var response2 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .PostJsonAsync(new AddTodoDto(
        new AddTodoDataDto("Meeting", "there's a meeting you need to attend"),
        new Dictionary<string, string>()));

    var id1 = await response1.GetJsonAsync<Guid>();
    var id2 = await response2.GetJsonAsync<Guid>();

    var response3 = await flurlClient
      .Request($"/todo/{id1}/link/{id2}")
      .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", customerId)
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response3.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
  }

  [Test]
  public async Task ShouldNotAllowExpiredTokensInAddTodoRequest()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .AllowAnyHttpStatus()
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateExpiredToken())
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadKeyInAddTodoRequest()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .AllowAnyHttpStatus()
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateTokenWithBadKey())
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadIssuerInAddTodoRequest()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .AllowAnyHttpStatus()
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateTokenFromBadIssuer())
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }
  
  [Test]
  public async Task ShouldNotAllowTokensWithNoBearerToken()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .AllowAnyHttpStatus()
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(400);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Bad Request");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadlyFormattedBearerToken()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "Bearer lol")
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithNoBearerInAuthorizationHeader()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "")
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(400);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Bad Request");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithNullAuthorizationHeader()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", null)
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(400);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Bad Request");
  }
}
