using System.Text.Json.Serialization;

namespace TodoApp
{
  public class TodoDto
  {
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("content")]
    public string Content { get; set; }
  }
}