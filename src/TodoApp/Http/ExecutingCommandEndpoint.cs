using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic;

namespace TodoApp.Http;

public class ExecutingCommandEndpoint<TDto, TResponse> : IAsyncEndpoint
{
  private readonly IRequestParser<TDto> _requestParser;
  private readonly ITodoCommandFactory<TDto, TResponse> _todoCommandFactory;
  private readonly IResponseInProgressFactory<TResponse> _responseInProgressFactory;

  public ExecutingCommandEndpoint(
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
    var data = await _requestParser.ParseAsync(request, cancellationToken);
    var responseInProgress = _responseInProgressFactory.CreateResponseInProgress(response);
    var command = _todoCommandFactory.CreateCommand(data, responseInProgress);
    await command.ExecuteAsync(cancellationToken);
  }
}