using System.Net;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.HttpContextMock;
using TodoApp.Http;
using static TddXt.AnyRoot.Root;

namespace TodoAppSpecification;

class TokenValidationActionSpecification
{
  [Test]
  public void ShouldPassControlFurtherWhenValidToken()
  {
    //GIVEN
    var innerAction = Substitute.For<IAsyncEndpoint>();
    var tokenValidationAction = new TokenValidatingEndpoint(innerAction);
    var contextMock = HttpContextMock.Default();
    var request = contextMock.Request()
      .WithOAuthBearerToken(TokenValidatingEndpoint.RequiredToken)
      .RealInstance;
    var realInstance = contextMock.Response().RealInstance;
    var cancellationToken = Any.Instance<CancellationToken>();

    //WHEN
    tokenValidationAction.ExecuteAsync(
      request,
      realInstance,
      cancellationToken);

    //THEN
    innerAction.Received(1).ExecuteAsync(request, realInstance, cancellationToken);
  }

  [Test]
  public void ShouldShortCircuitControlWith401ErrorWhenTokenInvalid()
  {
    //GIVEN
    var innerAction = Substitute.For<IAsyncEndpoint>();
    var tokenValidationAction = new TokenValidatingEndpoint(innerAction);
    var contextMock = HttpContextMock.Default();
    var cancellationToken = Any.Instance<CancellationToken>();
    var request = contextMock.Request()
      .WithOAuthBearerToken(Any.OtherThan(TokenValidatingEndpoint.RequiredToken))
      .RealInstance;

    //WHEN
    tokenValidationAction.ExecuteAsync(
      request,
      contextMock.Response().RealInstance,
      cancellationToken
    );

    //THEN
    innerAction.DidNotReceiveWithAnyArgs().ExecuteAsync(default, default, default);
    contextMock.Response().Should().HaveStatusCode(HttpStatusCode.Unauthorized);
  }
}