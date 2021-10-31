using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApp.Logic;
using TodoApp.Logic.TodoNotes;

namespace TodoApp.Http;

public class ExecutingCommandEndpoint<TDto, TResponse> : IAsyncEndpoint
{
  private readonly IRequestParser<TDto> _requestParser;
  private readonly ICommandFactory<TDto, TResponse> _todoCommandFactory;
  private readonly IResponseInProgressFactory<TResponse> _responseInProgressFactory;

  public ExecutingCommandEndpoint(
    IRequestParser<TDto> requestParser, 
    ICommandFactory<TDto, TResponse> todoCommandFactory, 
    IResponseInProgressFactory<TResponse> responseInProgressFactory)
  {
    _requestParser = requestParser;
    _todoCommandFactory = todoCommandFactory;
    _responseInProgressFactory = responseInProgressFactory;
  }

  public async Task HandleAsync(HttpRequest request, HttpResponse response,
    CancellationToken cancellationToken)
  {
    var data = await _requestParser.ParseAsync(request, cancellationToken);
    var responseInProgress = _responseInProgressFactory.CreateResponseInProgress(response);
    var command = _todoCommandFactory.CreateCommand(data, responseInProgress);
    await command.ExecuteAsync(cancellationToken);
  }
}