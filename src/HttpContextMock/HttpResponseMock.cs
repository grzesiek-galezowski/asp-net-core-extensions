using System.IO;
using Microsoft.AspNetCore.Http;

namespace TddXt.HttpContextMock;

public class HttpResponseMock
{
  private readonly IHttpContextMockSerialization _serialization;
  public HttpResponse RealInstance { get; }

  public HttpResponseMock(HttpResponse response, IHttpContextMockSerialization serialization)
  {
    _serialization = serialization;
    RealInstance = response;
  }

  public string BodyString()
  {
    RealInstance.Body.Position = 0;
    using var streamReader = new StreamReader(RealInstance.Body);
    var content = streamReader.ReadToEnd();
    return content;
  }

  public T? BodyObject<T>()
  {
    var content = BodyString();
    var deserialized = _serialization.Deserialize<T>(content);
    return deserialized;
  }
}