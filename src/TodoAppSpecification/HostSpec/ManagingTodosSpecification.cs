using System.Threading.Tasks;
using Flurl.Http;
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
      .WithHeader("Authorization", "Bearer trolololo")
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    var response2 = await flurlClient
      .Request("/todo")
      .WithHeader("Authorization", "Bearer trolololo")
      .AllowAnyHttpStatus()
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

    var id1 = (await response1.GetJsonAsync()).id;
    var id2 = (await response2.GetJsonAsync()).id;

    var response3 = await flurlClient
      .Request($"/todo/{id1}/link/{id2}")
      .WithHeader("Authorization", "Bearer trolololo")
      .PostJsonAsync(new { title = "Meeting", content="there's a meeting you need to attend"});

  }
}