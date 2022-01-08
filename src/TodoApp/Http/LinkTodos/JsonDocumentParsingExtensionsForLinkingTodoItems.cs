using System;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.ParsingJson;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http.LinkTodos;

internal static class JsonDocumentParsingExtensionsForLinkingTodoItems
{
  public static Guid Id2(this HttpRequest request)
  {
    return Guid.Parse(JsonParsingExtensions.RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id2)));
  }

  public static Guid Id1(this HttpRequest request)
  {
    return Guid.Parse(JsonParsingExtensions.RequiredStringFromRoute(request, nameof(LinkTodosRequestData.Id1)));
  }
}