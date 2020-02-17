using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using FluentAssertions.Primitives;
using Microsoft.Extensions.Primitives;
using TddXt.HttpContextMock.Internal;

namespace TddXt.HttpContextMock
{
  public class HttpResponseMockAssertions : ReferenceTypeAssertions<HttpResponseMock, HttpResponseMockAssertions>
  {
    public HttpResponseMockAssertions(HttpResponseMock instance)
    {
      Subject = instance;
    }

    public AndConstraint<HttpResponseMockAssertions> ContainHeader(string key, string value)
    {
      Subject.RealInstance.Headers.Should().Contain(key, value);
      return new AndConstraint<HttpResponseMockAssertions>(this);
    }

    public AndConstraint<HttpResponseMockAssertions> HaveStatusCode(HttpStatusCode code)
    {
      Subject.RealInstance.StatusCode.Should().Be((int)code);
      return new AndConstraint<HttpResponseMockAssertions>(this);
    }

    public AndConstraint<HttpResponseMockAssertions> ContainHeaders((string key, string value)[] args)
    {
      var keyValuePairs = args.Select(tuple => new KeyValuePair<string, StringValues>(tuple.key, tuple.value));
      Subject.RealInstance.Headers.Should().Contain(keyValuePairs);
      return new AndConstraint<HttpResponseMockAssertions>(this);
    }

    public AndConstraint<HttpResponseMockAssertions> ContainHeaders(object o)
    {
      var keyValuePairs = HttpHeadersFromObject.ExtractHeadersKeyValuePairs(o);

      Subject.RealInstance.Headers.Should().Contain(keyValuePairs);
      return new AndConstraint<HttpResponseMockAssertions>(this);
    }

    public AndConstraint<HttpResponseMockAssertions> HaveBody<T>(T expected)
    {
      var deserialized = Subject.BodyObject<T>();
      deserialized.Should().BeEquivalentTo(expected, 
        options => options
          .RespectingRuntimeTypes()
          .IncludingAllRuntimeProperties()
          .IncludingFields()
          .IncludingNestedObjects());
      return new AndConstraint<HttpResponseMockAssertions>(this);
    }

    protected override string Identifier { get; } = "HttpResponse";
  }
}