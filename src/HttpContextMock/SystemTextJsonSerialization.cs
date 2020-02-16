using System.Text.Json;

namespace TddXt.HttpContextMock
{
  public class SystemTextJsonSerialization : IHttpContextMockSerialization
  {
    public string Serialize<T>(T dto)
    {
      return JsonSerializer.Serialize(dto);
    }

    public T Deserialize<T>(string content)
    {
      return JsonSerializer.Deserialize<T>(content);
    }
  }
}