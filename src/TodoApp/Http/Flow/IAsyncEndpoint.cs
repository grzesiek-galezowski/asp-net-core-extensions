using System.Threading;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.Flow;

public interface IAsyncEndpoint
{
  Task Handle(HttpRequest request, HttpResponse response, CancellationToken cancellationToken);
}