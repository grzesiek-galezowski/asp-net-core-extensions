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
      title = "a",
      content = "b"
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