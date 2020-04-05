using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
    public interface IAsyncAction
    {
        Task ExecuteAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken);
    }
}