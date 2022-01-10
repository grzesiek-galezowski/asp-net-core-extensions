using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TddXt.HttpContextMock;
using TodoApp.Bootstrap;
using TodoApp.Http;
using TodoApp.Http.AddTodo;

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
    var dto = new AddTodoDto(new AddTodoDataDto("a", "b"), new Dictionary<string, string>());
      //bug add files - transcription will be added to the saved note
      //bug the content will be truncated when it reaches 200 chars
      //bug and the truncated note will be appended to the response?
    
    await serviceLogicRoot.AddTodoEndpoint.Handle(
      context.Request()
        .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
        .WithHeader("Content-Type", MediaTypeNames.Application.Json)
        .WithHeader("Accept", MediaTypeNames.Application.Json)
        .WithQueryParam("customerId", Any.String())
        .PostJson(dto)
        .RealInstance, 
      context.Response().RealInstance,
      new CancellationToken());

    //THEN
    context.Response().Should().HaveStatusCode(HttpStatusCode.OK);
    context.Response().BodyObject<Guid>().ToString().Should().NotBeEmpty();
  }

  //bug some integration-level error response specifications
}