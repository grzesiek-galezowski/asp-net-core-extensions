using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NullableReferenceTypesExtensions;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http;

public interface IRequestParser<T>
{
  Task<T> ParseAsync(HttpRequest request, CancellationToken cancellationToken);
}

public class JsonDocumentBasedRequestParser 
  : IRequestParser<CreateTodoRequestData>,
    IRequestParser<LinkTodosRequestData>
{
  async Task<CreateTodoRequestData> IRequestParser<CreateTodoRequestData>.
    ParseAsync(HttpRequest request, CancellationToken cancellationToken)
  {
    using var doc = await JsonDocument.ParseAsync(request.Body, cancellationToken: cancellationToken);
    var structure =
      new AddTodoRootElementParser(
        new AddTodoDataParser(
          new TitleParser(),
          new ContentParser()
        ),
        new LinksParser()
      );

      //bug try another approach based on extension methods (e.g. GetData().GetContent())
      //bug try another approach based on lambdas, e.g.
      //root.Enter("data", data => {result.Title = data.GetProperty("Title").Deserialize<int>()});
      var dto = structure.Parse(doc);
      return new CreateTodoRequestData(dto.Data.Title, dto.Data.Content);

  }

  Task<LinkTodosRequestData> IRequestParser<LinkTodosRequestData>.ParseAsync(HttpRequest request,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(new LinkTodosRequestData(request.Id1(), request.Id2()));
  }
}

internal class LinksParser
{
  public Dictionary<string, string> Parse(JsonElement jsonElement)
  {
    return jsonElement.GetProperty("links").Deserialize<Dictionary<string, string>>().OrThrow();
  }
}

internal class ContentParser
{
  public string Parse(JsonElement jsonElement)
  {
    return jsonElement.JsonProperty("content").GetString().OrThrow();
  }
}

internal class TitleParser
{
  public string Parse(JsonElement jsonElement)
  {
    return jsonElement.JsonProperty("title").GetString().OrThrow();
  }
}

internal class AddTodoDataParser
{
  private readonly TitleParser _titleParser;
  private readonly ContentParser _contentParser;

  public AddTodoDataParser(TitleParser titleParser, ContentParser contentParser)
  {
    _titleParser = titleParser;
    _contentParser = contentParser;
  }

  public AddTodoDataDto Parse(JsonElement jsonElement)
  {
    return new AddTodoDataDto(
      _titleParser.Parse(jsonElement.JsonProperty("data")),
      _contentParser.Parse(jsonElement.JsonProperty("data")));
  }
}

internal class AddTodoRootElementParser
{
  private readonly AddTodoDataParser _addTodoDataParser;
  private readonly LinksParser _linksParser;

  public AddTodoRootElementParser(AddTodoDataParser addTodoDataParser, LinksParser linksParser)
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

internal record AddTodoDto(AddTodoDataDto Data, Dictionary<string, string> Links);

internal record AddTodoDataDto(string Title, string Content);
