using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TddXt.HttpContextMock;

public class HttpContextMock
{
  private readonly IHttpContextMockSerialization _serialization;
    
  public HttpContext RealInstance { get; }

  public static HttpContextMock Default()
  {
    var serviceCollection = new ServiceCollection()
      .AddSingleton<ILoggerFactory>(ctx => new LoggerFactory());
    var defaultHttpContext = new DefaultHttpContext
    {
      Response =
      {
        Body = new MemoryStream()
      },
      RequestServices = serviceCollection.BuildServiceProvider()
    };

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