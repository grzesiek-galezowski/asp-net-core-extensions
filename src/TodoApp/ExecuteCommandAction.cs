using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic.App;

namespace TodoApp
{
  public class ExecuteCommandAction<TDto, TResponse> : IAsyncAction
    {
      private readonly IRequestParser<TDto> _requestParser;
        private readonly ITodoCommandFactory<TDto, TResponse> _todoCommandFactory;
        private readonly IResponseInProgressFactory<TResponse> _responseInProgressFactory;

        public ExecuteCommandAction(
          IRequestParser<TDto> requestParser, 
          ITodoCommandFactory<TDto, TResponse> todoCommandFactory, 
          IResponseInProgressFactory<TResponse> responseInProgressFactory)
        {
          _requestParser = requestParser;
          _todoCommandFactory = todoCommandFactory;
          _responseInProgressFactory = responseInProgressFactory;
        }

        public async Task ExecuteAsync(HttpRequest request, HttpResponse response,
          CancellationToken cancellationToken)
        {
          //bug cancellation token
          var dto = await _requestParser.ParseAsync(request);
          var responseInProgress = _responseInProgressFactory.CreateResponseInProgress(response);
          var command = _todoCommandFactory.CreateCommand(dto, responseInProgress);
          await command.ExecuteAsync(cancellationToken);
        }
    }
}