using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.ParsingJson;

public interface IRequestParser<T>
{
  Task<T> Parse(HttpRequest request, CancellationToken cancellationToken);
}