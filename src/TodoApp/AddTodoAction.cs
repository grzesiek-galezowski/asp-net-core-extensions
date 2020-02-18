using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoApp
{
  public interface ITodoCommandFactory<TDto, TResponse>
  {
    AddTodoCommand CreateCommand(TDto dto, TResponse responseInProgress);
  }

  public class TodoCommandFactory : ITodoCommandFactory<TodoDto, IAddTodoResponseInProgress>
  {
    private readonly IIdGenerator _idGenerator;

    public TodoCommandFactory(IIdGenerator idGenerator)
    {
      _idGenerator = idGenerator;
    }

    public AddTodoCommand CreateCommand(TodoDto dto, IAddTodoResponseInProgress responseInProgress)
    {
      return new AddTodoCommand(dto, _idGenerator, responseInProgress);
    }
  }

  public class AddTodoAction<TDto, TResponse> : IAsyncAction
    {
      private readonly IRequestParser<TDto> _requestParser;
        private readonly ITodoCommandFactory<TDto, TResponse> _todoCommandFactory;
        private readonly IResponseInProgressFactory<TResponse> _responseInProgressFactory;

        public AddTodoAction(
          IRequestParser<TDto> requestParser, 
          ITodoCommandFactory<TDto, TResponse> todoCommandFactory, 
          IResponseInProgressFactory<TResponse> responseInProgressFactory)
        {
          _requestParser = requestParser;
          _todoCommandFactory = todoCommandFactory;
          _responseInProgressFactory = responseInProgressFactory;
        }

        public async Task ExecuteAsync(HttpRequest request, HttpResponse response)
        {
          //bug cancellation token
            var dto = await _requestParser.ParseAsync(request);
            var responseInProgress = _responseInProgressFactory.CreateResponseInProgress(response);
            await _todoCommandFactory.CreateCommand(dto, responseInProgress).ExecuteAsync();
        }
    }
}