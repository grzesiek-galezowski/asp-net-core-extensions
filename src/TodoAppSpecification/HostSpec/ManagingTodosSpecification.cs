﻿using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

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
      .WithHeader("Authorization", "Bearer trolololo")
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});
  }

  [Test]
  public async Task ShouldBeAbleToLinkTodos()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateToken())
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    var response2 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    var id1 = await response1.GetJsonAsync<Guid>();
    var id2 = await response2.GetJsonAsync<Guid>();

    var response3 = await flurlClient
      .Request($"/todo/{id1}/link/{id2}")
      .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
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
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadlyFormattedBearerToken()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "Bearer lol")
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
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldNotAllowTokensWithNullAuthorizationHeader()
  {
    await using var appFactory = new AppFactory();
    var flurlClient = new FlurlClient(appFactory.CreateClient());

    var response1 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", null)
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    response1.StatusCode.Should().Be(401);
    response1.ResponseMessage.ReasonPhrase.Should().Be("Unauthorized");
  }
}
