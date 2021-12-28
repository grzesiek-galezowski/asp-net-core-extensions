using System.Text.Json;
using NullableReferenceTypesExtensions;

namespace TodoApp.Http;

internal class TitleParser
{
  public string Parse(JsonElement jsonElement)
  {
    return jsonElement.JsonProperty("Title").GetString().OrThrow();
  }
}