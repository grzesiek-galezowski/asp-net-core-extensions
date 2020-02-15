using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
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
  }


}