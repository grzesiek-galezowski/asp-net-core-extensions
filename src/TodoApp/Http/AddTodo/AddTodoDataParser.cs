using System.Text.Json;
using TodoApp.Http.ParsingJson;

namespace TodoApp.Http.AddTodo;

internal class AddTodoDataParser : IJsonElementParser<AddTodoDataDto>
{
  private readonly IJsonElementParser<string> _titleParser;
  private readonly IJsonElementParser<string> _contentParser;

  public AddTodoDataParser(
    IJsonElementParser<string> titleParser, 
    IJsonElementParser<string> contentParser)
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