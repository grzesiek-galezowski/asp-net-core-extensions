using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace TddXt.HttpContextMock
{
  public class HttpResponseMock
  {
    public HttpResponse RealInstance { get; }

    public HttpResponseMock(HttpResponse response)
    {
      RealInstance = response;
    }

    public string BodyString()
    {
      using var streamReader = new StreamReader(RealInstance.Body);
      var content = streamReader.ReadToEnd();
      return content;
    }

    public T BodyObject<T>()
    {
      var content = BodyString();
      var deserialized = JsonSerializer.Deserialize<T>(content);
      return deserialized;
    }
  }
}