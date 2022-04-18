using System;
using System.Text.Json;
using Core.NullableReferenceTypesExtensions;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.ParsingJson;

public static class JsonParsingExtensions
{
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