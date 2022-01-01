using System;
using System.Text.Json;
using Humanizer;
using Microsoft.AspNetCore.Http;
using NullableReferenceTypesExtensions;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http;

public static class JsonDocumentRouteValuesExtensions
{
  public static Guid Id2(this HttpRequest request)
  {
    return Guid.Parse(RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id2)));
  }

  public static Guid Id1(this HttpRequest request)
  {
    return Guid.Parse(RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id1)));
  }

  public static string RequiredStringFromRoute(HttpRequest request, string propertyName)
  {
    return request.RouteValues[propertyName.Camelize()].OrThrow().ToString().OrThrow();
  }

  public static JsonElement JsonProperty(this JsonElement element, string propertyName)
  {
    if (element.TryGetProperty(propertyName, out var value))
    {
      return value.OrThrow();
    }
    else
    {
      throw new Exception($"Missing key [{propertyName}] in {element.GetRawText()}"); //bug better exception
    }
  }
}