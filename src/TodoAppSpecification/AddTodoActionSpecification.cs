using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TddXt.HttpContextMock;
using TodoApp.Bootstrap;

namespace TodoAppSpecification;

public class AddTodoActionSpecification
{
  [Test]
  public async Task ShouldRespondToPostTodoWithTodoCreated()
  {
    //GIVEN
    await using var serviceLogicRoot = new ServiceLogicRoot(new TokenValidationParameters
    {
      ValidIssuer = TestTokens.Issuer,
      IssuerSigningKey = TestTokens.SecurityKey
    }, Any.Instance<ILoggerFactory>());
    var context = HttpContextMock.Default();
    var dto = new 
    {
      data = new {
        title = "a",
        content = "b"
      },
      links = new Dictionary<string, string>() //TODO no duplicates allowed
      //bug add files - transcription will be added to the saved note
      //bug the content will be truncated when it reaches 200 chars
    };
    
    await serviceLogicRoot.AddTodoEndpoint.HandleAsync(
      context.Request()
        .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
        .PostJson(dto)
        .RealInstance, 
      context.Response().RealInstance,
      new CancellationToken());

    //THEN
    context.Response().Should().HaveStatusCode(HttpStatusCode.OK);
    context.Response().BodyObject<Guid>().ToString().Should().NotBeEmpty();
  }
}