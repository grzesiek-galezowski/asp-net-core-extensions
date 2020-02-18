using System.Text.Json.Serialization;

namespace TodoApp.App
{
  public class TodoCreatedDto
  {
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("content")]
    public string Content { get; set; }
  }
}