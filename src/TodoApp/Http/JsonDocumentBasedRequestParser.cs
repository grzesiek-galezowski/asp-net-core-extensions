using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using NullableReferenceTypesExtensions;
using TodoApp.Logic;

namespace TodoApp.Http;

public interface IRequestParser<T>
{
  Task<T> ParseAsync(HttpRequest request, CancellationToken cancellationToken);
}

public class JsonDocumentBasedRequestParser 
  : IRequestParser<CreateTodoRequestData>,
    IRequestParser<LinkTodosRequestData>
{
  public async Task<CreateTodoRequestData> ParseAsync(HttpRequest request, CancellationToken cancellationToken)
  {
    using var doc = await JsonDocument.ParseAsync(request.Body, cancellationToken: cancellationToken);
    return new CreateTodoRequestData(
      Title(doc),
      Content(doc));
  }

  Task<LinkTodosRequestData> IRequestParser<LinkTodosRequestData>.ParseAsync(HttpRequest request,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(new LinkTodosRequestData(
      Id1(request), 
      Id2(request)));
  }

  private static string Content(JsonDocument doc)
  {
    return RequiredStringFromBody(doc, nameof(CreateTodoRequestData.Content));
  }

  private static string Title(JsonDocument doc)
  {
    return RequiredStringFromBody(doc, nameof(CreateTodoRequestData.Title));
  }

  private static Guid Id2(HttpRequest request)
  {
    return Guid.Parse(RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id2)));
  }

  private static Guid Id1(HttpRequest request)
  {
    return Guid.Parse(RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id1)));
  }

  private static string RequiredStringFromRoute(HttpRequest request, string propertyName)
  {
    return request.RouteValues[propertyName.Camelize()].OrThrow().ToString().OrThrow();
  }

  private static string RequiredStringFromBody(JsonDocument doc, string propertyName)
  {
    var camelizedPropertyName = propertyName.Camelize();
    if (doc.RootElement.TryGetProperty(camelizedPropertyName, out var value))
    {
      return value.GetString().OrThrow();
    }
    else
    {
      throw new Exception($"Missing key [{camelizedPropertyName}] in {doc.ToJsonString()}"); //bug better exception
    }
  }
}