using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
    public class TokenValidationAction : IAsyncAction
    {
        public TokenValidationAction(AddTodoAction addTodoAction)
        {
            throw new System.NotImplementedException();
        }

        public Task ExecuteAsync(HttpRequest request, HttpResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}