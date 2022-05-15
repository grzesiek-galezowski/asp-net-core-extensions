using System.Threading;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.ParsingJson;
using TodoApp.Logic.TodoNotes;

namespace TodoApp.Http.Flow;

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

  public async Task Handle(
    HttpRequest request, 
    HttpResponse response,
    CancellationToken cancellationToken)
  {
    var data = await _requestParser.Parse(request, cancellationToken);
    var responseInProgress = _responseInProgressFactory.CreateResponseInProgress(response);
    var command = _todoCommandFactory.CreateCommand(data, responseInProgress);
    await command.Execute(cancellationToken);
  }
}