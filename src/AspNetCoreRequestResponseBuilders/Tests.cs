using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Strings;
using TddXt.HttpContextMock;
using static TddXt.AnyRoot.Root;

namespace HttpContextMockSpecification
{
  public class Tests
  {
    [Test]
    public void Test1()
    {
      var httpContextMock = HttpContextMock.Default();
      httpContextMock.Request()
        .WithHeader("Accept", "text/plain")
        .WithHeader(new {Accept = MediaTypeNames.Text.Plain})
        .Https()
        .AppendPathSegment("person")
        .AppendPathSegments("person2", "person3")
        .AppendPathSegments(new List<string> {"person4"})
        .AppendPathSegment("person5")
        .WithStringBody("lolek")
        .WithJsonBody(new  { A = 123, B = "SAA"})
        .WithOAuthBearerToken("my_oauth_token")
        .WithBasicAuth("password")
        .WithQueryParams(new {a = 1, b = 2})
        .WithQueryParam("c", "x");
      //todo https://flurl.dev/docs/fluent-http/
      Assert.AreEqual("{\"A\":123,\"B\":\"SAA\"}", new StreamReader(httpContextMock.RealInstance.Request.Body).ReadToEnd());
    }

    [Test]
    public void ShouldContainEmptyBodyInResponse()
    {
      var httpContextMock = HttpContextMock.Default();
      httpContextMock.Response().BodyString().Should().BeEmpty();
    }
    
    [Test]
    public void ShouldAllowSettingBytesBody()
    {
      var httpContextMock = HttpContextMock.Default();
      var content = Any.Array<byte>(3);
      httpContextMock.Request().WithBytesBody(content);

      var result = new List<byte>
      {
        (byte)httpContextMock.Request().RealInstance.Body.ReadByte(),
        (byte)httpContextMock.Request().RealInstance.Body.ReadByte(),
        (byte)httpContextMock.Request().RealInstance.Body.ReadByte()
      };
      result.Should().Equal(content);
      httpContextMock.Request().RealInstance.Body.Length.Should().Be(3);
    }
    
    [Test]
    public void ShouldAllowSettingQueryViaQueryCollection()
    {
      var httpContextMock = HttpContextMock.Default();
      var name = Any.String();
      var value = Any.String();
      httpContextMock.Request().WithQuery(new Dictionary<string, StringValues>
      {
        [name] = value
      });

      httpContextMock.Request().RealInstance.Query[name].Should().BeEquivalentTo(new StringValues(value));
    }

    [Test]
    public void ShouldAllowSettingQueryViaDictionary()
    {
      var httpContextMock = HttpContextMock.Default();
      var name = Any.String();
      var value = Any.String();
      httpContextMock.Request().WithQuery(new QueryCollection(new Dictionary<string, StringValues>
      {
        [name] = value
      }));

      httpContextMock.Request().RealInstance.Query[name].Should().BeEquivalentTo(new StringValues(value));
    }
    
    [Test]
    public void ShouldAllowRewindingRequestBody()
    {
      var httpContextMock = HttpContextMock.Default();
      httpContextMock.Request().WithStringBody(Any.String());
      using var streamReader = new StreamReader(httpContextMock.Request().RealInstance.Body);
      using var streamReader2 = new StreamReader(httpContextMock.Request().RealInstance.Body);
      var content = streamReader.ReadToEnd();
      httpContextMock.Request().RewindBody();
      var content2 = streamReader2.ReadToEnd();
      content.Should().Be(content2);
    }
  }
}