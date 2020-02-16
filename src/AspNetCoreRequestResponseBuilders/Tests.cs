using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using FluentAssertions;
using NUnit.Framework;
using TddXt.HttpContextMock;

namespace HttpContextMockSpecification
{
  public class Tests
  {
    [Test]
    public void Test1()
    {
      var httpContextMock = new HttpContextMock();
      httpContextMock.Request()
        .WithHeader("Accept", "text/plain")
        .WithHeader(new {Accept = MediaTypeNames.Text.Plain})
        .Https()
        .AppendPathSegment("person")
        .AppendPathSegments("person2", "person3")
        .AppendPathSegments(new List<string> {"person4"})
        .AppendPathSegment("person5")
        .SetStringBody("lolek")
        .SetJsonBody(new  { A = 123, B = "SAA"})
        .WithOAuthBearerToken("my_oauth_token")
        .WithBasicAuth("password")
        .SetQueryParams(new {a = 1, b = 2})
        .SetQueryParam("c", "x");
      //todo https://flurl.dev/docs/fluent-http/
      Assert.AreEqual("{\"A\":123,\"B\":\"SAA\"}", new StreamReader(httpContextMock.RealInstance.Request.Body).ReadToEnd());
    }

    [Test]
    public void Mock()
    {
      var httpContextMock = new HttpContextMock();
      httpContextMock.Response().BodyString().Should().BeEmpty();
    }
  }
}