using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.HttpContextMock;
using TodoApp;
using static TddXt.AnyRoot.Root;

namespace TodoAppSpecification
{
  class TokenValidationActionSpecification
  {
    [Test]
    public void ShouldPassControlFurtherWhenValidToken()
    {
      //GIVEN
      var innerAction = Substitute.For<IAsyncAction>();
      var tokenValidationAction = new TokenValidationAction(innerAction);
      var contextMock = HttpContextMock.Default();
      var request = contextMock.Request()
        .WithOAuthBearerToken(TokenValidationAction.RequiredToken)
        .RealInstance;
      var realInstance = contextMock.Response().RealInstance;

      //WHEN
      tokenValidationAction.ExecuteAsync(
        request,
        realInstance);

      //THEN
      innerAction.Received(1).ExecuteAsync(request, realInstance);
    }

    [Test]
    public void ShouldShortCircuitControlWith401ErrorWhenTokenInvalid()
    {
      //GIVEN
      var innerAction = Substitute.For<IAsyncAction>();
      var tokenValidationAction = new TokenValidationAction(innerAction);
      var contextMock = HttpContextMock.Default();
      var request = contextMock.Request()
        .WithOAuthBearerToken(Any.OtherThan(TokenValidationAction.RequiredToken))
        .RealInstance;

      //WHEN
      tokenValidationAction.ExecuteAsync(
        request,
        contextMock.Response().RealInstance);

      //THEN
      innerAction.DidNotReceiveWithAnyArgs().ExecuteAsync(default, default);
      contextMock.Response().Should().HaveStatusCode(HttpStatusCode.Unauthorized);
    }
  }
}
