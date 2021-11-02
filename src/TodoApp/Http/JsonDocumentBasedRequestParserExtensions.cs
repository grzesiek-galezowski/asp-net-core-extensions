using System;
using System.Text.Json;
using Humanizer;
using Microsoft.AspNetCore.Http;
using NullableReferenceTypesExtensions;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http;

public static class JsonDocumentBasedRequestParserExtensions
{
  public static string Content(this JsonDocument doc)
  {
    return JsonElement(doc, nameof(CreateTodoRequestData.Content)).GetString().OrThrow();
  }

  public static string Title(this JsonDocument doc)
  {
    return JsonElement(doc, nameof(CreateTodoRequestData.Title)).GetString().OrThrow();
  }

  public static Guid Id2(this HttpRequest request)
  {
    return Guid.Parse((string)RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id2)));
  }

  public static Guid Id1(this HttpRequest request)
  {
    return Guid.Parse((string)RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id1)));
  }

  private static string RequiredStringFromRoute(HttpRequest request, string propertyName)
  {
    return request.RouteValues[propertyName.Camelize()].OrThrow().ToString().OrThrow();
  }

  public static JsonElement JsonElement(this JsonDocument doc, string propertyName)
  {
    var camelizedPropertyName = propertyName.Camelize();
    if (doc.RootElement.TryGetProperty(camelizedPropertyName, out var value))
    {
      return value.OrThrow();
    }
    else
    {
      throw new Exception($"Missing key [{camelizedPropertyName}] in {doc.ToJsonString()}"); //bug better exception
    }
  }
}