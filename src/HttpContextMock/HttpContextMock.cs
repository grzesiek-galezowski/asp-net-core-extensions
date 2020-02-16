using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;

namespace TddXt.HttpContextMock
{
  public class HttpContextMock
  {
    public HttpContext RealInstance { get; }

    public HttpContextMock()
    {
      RealInstance = new DefaultHttpContext();
    }

    public HttpRequestMock Request()
    {
      return new HttpRequestMock(RealInstance.Request);
    }

    public HttpResponseMock Response()
    {
      return new HttpResponseMock(RealInstance.Response);
    }
  }
}