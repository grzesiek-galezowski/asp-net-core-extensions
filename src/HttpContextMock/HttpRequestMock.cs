using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using TddXt.HttpContextMock.Internal;

namespace TddXt.HttpContextMock
{
  public class HttpRequestMock
  {
    private readonly IHttpContextMockSerialization _serialization;
    public HttpRequest RealInstance { get; }

    public HttpRequestMock(HttpRequest request, IHttpContextMockSerialization serialization)
    {
      RealInstance = request;
      _serialization = serialization;
    }

    public HttpRequestMock WithStringBody(string content)
    {
      return WithBytesBody(Encoding.UTF8.GetBytes(content));
    }

    public HttpRequestMock WithJsonBody<T>(T dto)
    {
      return WithContentType(MediaTypeNames.Application.Json).WithStringBody(_serialization.Serialize(dto));
    }
    
    public HttpRequestMock WithBytesBody(byte[] content)
    {
      RealInstance.Body = new MemoryStream(content);
      return this;
    }

    public HttpRequestMock WithPlainTextBody(string content)
    {
      return WithContentType(MediaTypeNames.Text.Plain).WithStringBody(content);
    }

    public HttpRequestMock AppendPathSegment(string segment)
    {
      RealInstance.Path = RealInstance.Path.Add("/" + segment);
      return this;
    }

    public HttpRequestMock WithOAuthBearerToken(string token)
    {
      return WithAuthorization("Bearer", token);
    }

    public HttpRequestMock WithBasicAuth(string password)
    {
      return WithAuthorization("Basic", password);
    }

    public HttpRequestMock WithAuthorization(string type, string content)
    {
      return WithHeader("authorization", $"{type} {content}");
    }

    public HttpRequestMock WithQueryParams(object o)
    {
      foreach (var propertyInfo in o.GetType().GetProperties())
      {
        WithQueryParam(propertyInfo.Name, propertyInfo.GetValue(o).ToString());
      }
      return this;
    }

    public HttpRequestMock WithQueryParam(string key, string value)
    {
      RealInstance.QueryString = RealInstance.QueryString.Add(key, value);
      return this;
    }

    public HttpRequestMock AppendPathSegments(params string[] segments)
    {
      foreach (var segment in segments)
      {
        AppendPathSegment(segment);
      }

      return this;
    }

    public HttpRequestMock AppendPathSegments(IEnumerable<string> segments)
    {
      return AppendPathSegments(segments.ToArray());
    }

    public HttpRequestMock PostJson<T>(T dto)
    {
      return WithJsonBody(dto).WithMethod(HttpMethods.Post);
    }
    
    public HttpRequestMock PostPlainText(string text)
    {
      return WithPlainTextBody(text).WithMethod(HttpMethods.Post);
    }

    public HttpRequestMock PutJson<T>(T dto)
    {
      return WithJsonBody(dto).WithMethod(HttpMethods.Put);
    }

    public HttpRequestMock Put()
    {
      return WithMethod(HttpMethods.Put);
    }

    public HttpRequestMock Post()
    {
      return WithMethod(HttpMethods.Post);
    }

    public HttpRequestMock Get()
    {
      return WithMethod(HttpMethods.Get);
    }

    private HttpRequestMock WithMethod(string method)
    {
      RealInstance.Method = method;
      return this;
    }

    public HttpRequestMock WithContentType(string requestContentType)
    {
      RealInstance.ContentType = requestContentType;
      return this;
    }

    public HttpRequestMock WithHeader(string key, string value)
    {
      RealInstance.Headers[key] = value;
      return this;
    }

    public HttpRequestMock Accept(string value)
    {
      return WithHeader("accept", value);
    }

    public HttpRequestMock AcceptApplicationJson()
    {
      return Accept(MediaTypeNames.Application.Json);
    }

    public HttpRequestMock WithHeader(object properties)
    {
      foreach (var header in HttpHeadersFromObject.ExtractHeadersKeyValuePairs(properties))
      {
        WithHeader(header.Key, header.Value);
      }
      return this;
    }

    public HttpRequestMock Https()
    {
      RealInstance.IsHttps = true;
      return this;
    }

    public HttpRequestMock WithQuery(Dictionary<string, StringValues> dictionary)
    {
      return WithQuery(new QueryCollection(dictionary));
    }

    public HttpRequestMock WithQuery(QueryCollection queryCollection)
    {
      RealInstance.Query = queryCollection;
      return this;
    }

    public HttpRequestMock RewindBody()
    {
      RealInstance.Body.Position = 0;
      return this;
    }
  }
}