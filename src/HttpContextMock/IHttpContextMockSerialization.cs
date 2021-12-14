namespace TddXt.HttpContextMock;

public interface IHttpContextMockSerialization
{
  string Serialize<T>(T dto);
  T? Deserialize<T>(string content);
}