using System.Collections.Generic;
using System.Text.Json;

namespace TodoApp.Http;

internal class DictionaryParser
{
  private readonly string _propertyName;

  public DictionaryParser(string propertyName)
  {
    _propertyName = propertyName;
  }

  public Dictionary<string, string> Parse(JsonElement jsonElement)
  {
    return jsonElement.JsonProperty(_propertyName).Deserialize<Dictionary<string, string>>().OrThrow();
  }
}