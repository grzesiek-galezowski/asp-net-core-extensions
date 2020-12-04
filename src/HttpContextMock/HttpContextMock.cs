using System.IO;
using Microsoft.AspNetCore.Http;

namespace TddXt.HttpContextMock
{
  public class HttpContextMock
  {
    private readonly IHttpContextMockSerialization _serialization;
    
    public HttpContext RealInstance { get; }

    public static HttpContextMock Default()
    {
      var defaultHttpContext = new DefaultHttpContext();
      defaultHttpContext.Response.Body = new MemoryStream();
      return new HttpContextMock(defaultHttpContext, new SystemTextJsonSerialization());
    }

    public HttpContextMock(HttpContext context, IHttpContextMockSerialization serialization)
    {
      RealInstance = context;
      _serialization = serialization;
    }

    public HttpRequestMock Request() => new(RealInstance.Request, _serialization);
    public HttpResponseMock Response() => new(RealInstance.Response, _serialization);
  }
}