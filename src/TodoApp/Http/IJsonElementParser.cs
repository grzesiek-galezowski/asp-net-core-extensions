using System.Text.Json;

namespace TodoApp.Http;

public interface IJsonElementParser<out T>
{
  T Parse(JsonElement jsonElement);
}