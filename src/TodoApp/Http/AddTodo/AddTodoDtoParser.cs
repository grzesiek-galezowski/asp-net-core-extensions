using System.Collections.Generic;
using System.Text.Json;
using TodoApp.Http.ParsingJson;

namespace TodoApp.Http.AddTodo;

public class AddTodoDtoParser : IJsonElementParser<AddTodoDto>
{
  private readonly IJsonElementParser<AddTodoDataDto> _addTodoDataParser;
  private readonly IJsonElementParser<Dictionary<string, string>> _linksParser;

  public AddTodoDtoParser(
    IJsonElementParser<AddTodoDataDto> addTodoDataParser, 
    IJsonElementParser<Dictionary<string, string>> linksParser)
  {
    _addTodoDataParser = addTodoDataParser;
    _linksParser = linksParser;
  }

  public AddTodoDto Parse(JsonElement jsonElement)
  {
    return new AddTodoDto(
      _addTodoDataParser.Parse(jsonElement),
      _linksParser.Parse(jsonElement)
    );
  }
}