using System.Text.Json;

namespace TodoApp.Http;

internal class AddTodoDataParser
{
  private readonly RequiredStringParser _titleParser;
  private readonly RequiredStringParser _contentParser;

  public AddTodoDataParser(RequiredStringParser titleParser, RequiredStringParser contentParser)
  {
    _titleParser = titleParser;
    _contentParser = contentParser;
  }

  public AddTodoDataDto Parse(JsonElement jsonElement)
  {
    return new AddTodoDataDto(
      _titleParser.Parse(jsonElement.JsonProperty("Data")),
      _contentParser.Parse(jsonElement.JsonProperty("Data")));
  }
}