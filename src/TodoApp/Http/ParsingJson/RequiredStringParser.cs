using System.Text.Json;
using Core.NullableReferenceTypesExtensions;

namespace TodoApp.Http.ParsingJson;

internal class RequiredStringParser : IJsonElementParser<string>
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