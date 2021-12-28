using System.Text.Json;

namespace TodoApp.Http;

internal class AddTodoDtoParser
{
  private readonly AddTodoDataParser _addTodoDataParser;
  private readonly DictionaryParser _linksParser;

  public AddTodoDtoParser(AddTodoDataParser addTodoDataParser, DictionaryParser linksParser)
  {
    _addTodoDataParser = addTodoDataParser;
    _linksParser = linksParser;
  }

  public AddTodoDto Parse(JsonDocument doc)
  {
    return new AddTodoDto(
      _addTodoDataParser.Parse(doc.RootElement),
      _linksParser.Parse(doc.RootElement)
    );
  }
}