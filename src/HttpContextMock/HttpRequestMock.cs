using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TddXt.HttpContextMock
{
  public class HttpRequestMock
  {
    public HttpRequest RealInstance { get; }

    public HttpRequestMock(HttpRequest request)
    {
      RealInstance = request;
    }

    public HttpRequestMock SetStringBody(string content)
    {
      RealInstance.Body = new MemoryStream(Encoding.UTF8.GetBytes(content));
      return this;
    }

    public HttpRequestMock SetPlainTextBody(string content)
    {
      return SetContentType(MediaTypeNames.Text.Plain).SetStringBody(content);
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

    public HttpRequestMock SetQueryParams(object o)
    {
      foreach (var propertyInfo in o.GetType().GetProperties())
      {
        SetQueryParam(propertyInfo.Name, propertyInfo.GetValue(o).ToString());
      }
      return this;
    }

    public HttpRequestMock SetQueryParam(string key, string value)
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
      return SetJsonBody(dto).SetMethod(HttpMethods.Post);
    }
    
    public HttpRequestMock PostPlainText(string text)
    {
      return SetPlainTextBody(text).SetMethod(HttpMethods.Post);
    }

    public HttpRequestMock PutJson<T>(T dto)
    {
      return SetJsonBody(dto).SetMethod(HttpMethods.Put);
    }

    public HttpRequestMock Put()
    {
      return SetMethod(HttpMethods.Put);
    }

    public HttpRequestMock Post()
    {
      return SetMethod(HttpMethods.Post);
    }

    public HttpRequestMock Get()
    {
      return SetMethod(HttpMethods.Get);
    }

    private HttpRequestMock SetMethod(string method)
    {
      RealInstance.Method = method;
      return this;
    }

    public HttpRequestMock SetJsonBody<T>(T dto)
    {
      return SetContentType(MediaTypeNames.Application.Json).SetStringBody(
        System.Text.Json.JsonSerializer.Serialize(dto));
    }

    public HttpRequestMock SetContentType(string requestContentType)
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
      foreach (var propertyInfo in properties.GetType().GetProperties())
      {
        WithHeader(propertyInfo.Name.Replace("_", "-"), propertyInfo.GetValue(properties).ToString());
      }
      return this;
    }

    public HttpRequestMock Https()
    {
      RealInstance.IsHttps = true;
      return this;
    }
  }
}