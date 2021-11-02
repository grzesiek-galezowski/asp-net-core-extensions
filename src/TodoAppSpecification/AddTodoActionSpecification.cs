using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using TddXt.HttpContextMock;
using TodoApp.Bootstrap;
using TodoApp.Logic;

namespace TodoAppSpecification;

public class AddTodoActionSpecification
{
  [Test]
  public async Task ShouldRespondToPostTodoWithTodoCreated()
  {
    //GIVEN
    await using var serviceLogicRoot = new ServiceLogicRoot();
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