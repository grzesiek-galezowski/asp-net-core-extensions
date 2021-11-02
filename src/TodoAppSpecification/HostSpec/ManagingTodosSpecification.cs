using System;
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
}

public interface IX
{
  void Lol1();
  void Lol2();
}

public class X : IX
{
  public virtual void Lol1()
  {
  }

  public void Lol2()
  {
  }
}
