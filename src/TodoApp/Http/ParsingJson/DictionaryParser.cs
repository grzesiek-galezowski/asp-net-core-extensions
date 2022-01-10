using System.Collections.Generic;
using System.Text.Json;

namespace TodoApp.Http.ParsingJson;

internal class DictionaryParser<TKey, TValue> : 
  IJsonElementParser<Dictionary<TKey, TValue>> where TKey : notnull
{
  private readonly string _propertyName;

  public DictionaryParser(string propertyName)
  {
    _propertyName = propertyName;
  }

  public Dictionary<TKey, TValue> Parse(JsonElement jsonElement)
  {
    return jsonElement.JsonProperty(_propertyName).Deserialize<Dictionary<TKey, TValue>>().OrThrow();
  }
}