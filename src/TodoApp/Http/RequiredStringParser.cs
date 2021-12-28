using System.Text.Json;
using NullableReferenceTypesExtensions;

namespace TodoApp.Http;

internal class RequiredStringParser
{
  private readonly string _propertyName;

  public RequiredStringParser(string propertyName)
  {
    _propertyName = propertyName;
  }

  public string Parse(JsonElement jsonElement)
  {
    return jsonElement.JsonProperty(_propertyName).GetString().OrThrow();
  }
}