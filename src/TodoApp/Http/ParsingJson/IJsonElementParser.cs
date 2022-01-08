using System.Text.Json;

namespace TodoApp.Http.ParsingJson;

public interface IJsonElementParser<out T>
{
  T Parse(JsonElement jsonElement);
}