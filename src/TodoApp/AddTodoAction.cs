using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
    public class AddTodoAction : IAsyncAction
    {
        private readonly IIdGenerator _idGenerator;

        public AddTodoAction(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public async Task ExecuteAsync(HttpRequest request, HttpResponse response)
        {
            //bug cancellation token
            var dto = await JsonSerializer.DeserializeAsync<TodoDto>(request.Body);
            var id = _idGenerator.Generate();

            await JsonSerializer.SerializeAsync(response.Body,
                new TodoCreatedDto
                {
                    Content = dto.Content,
                    Title = dto.Title,
                    Id = id
                });
        }
    }
}