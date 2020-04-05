using System.Collections.Generic;
using System.Text.Json.Serialization;
using SmartAnalyzers.CSharpExtensions.Annotations;

namespace TodoApp.Logic.App
{
  [InitRequired]
  public class TodoCreatedDto
  {
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("content")] public string Content { get; set; }

    [JsonPropertyName("links")] public HashSet<string> Links { get; }

  }
}