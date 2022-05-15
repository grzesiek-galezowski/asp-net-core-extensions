using System.Threading;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.ParsingJson;

public interface IRequestParser<T>
{
  Task<T> Parse(HttpRequest request, CancellationToken cancellationToken);
}