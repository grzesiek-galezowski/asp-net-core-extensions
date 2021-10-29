using System;
using System.Text.Json;
using Humanizer;
using Microsoft.AspNetCore.Http;
using NullableReferenceTypesExtensions;
using TodoApp.Logic.AddTodo;
using TodoApp.Logic.LinkTodos;

namespace TodoApp.Http;

public static class JsonDocumentBasedRequestParserExtensions
{
  public static string Content(this JsonDocument doc)
  {
    return BasicNullableExtensions.OrThrow<string>(JsonElement(doc, nameof(CreateTodoRequestData.Content)).GetString());
  }

  public static string Title(this JsonDocument doc)
  {
    return BasicNullableExtensions.OrThrow<string>(JsonElement(doc, nameof(CreateTodoRequestData.Title)).GetString());
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

  private static JsonElement JsonElement(JsonDocument doc, string propertyName)
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