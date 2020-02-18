using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
  public interface IRequestParser<T>
  {
    Task<T> ParseAsync(HttpRequest request);
  }

  public class RequestParser<T> : IRequestParser<T>
  {
    public async Task<T> ParseAsync(HttpRequest request)
    {
      return await JsonSerializer.DeserializeAsync<T>(request.Body);
    }
  }
}