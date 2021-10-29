using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http;

public interface IAsyncEndpoint
{
  Task HandleAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken);
}