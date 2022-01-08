using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Http.AddTodo;
using TodoApp.Http.Flow;
using TodoApp.Http.HttpValidation;
using TodoApp.Http.LinkTodos;
using TodoApp.Http.ParsingJson;
using TodoApp.Http.Support;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Http;

public class EndpointsAdapter
{
  public EndpointsAdapter(
    ICommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress> addTodoCommandFactory,
    ICommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress> linkTodoCommandFactory,
    TokenValidationParameters tokenValidationParameters,
    IEndpointsSupport support)
  {
    LinkTodoEndpoint =
      StandardEndpoint(support,
        new AddTodoItemRequestProcessingPolicy(tokenValidationParameters),
        new ExecutingCommandEndpoint<LinkTodosRequestData, ILinkTodoResponseInProgress>(
          new LinkTodosRequestDataParser(),
          linkTodoCommandFactory,
          new ResponseInProgressFactory()));

    AddTodoEndpoint =
      StandardEndpoint(support,
        new LinkTodoItemsRequestProcessingPolicy(tokenValidationParameters),
        new ExecutingCommandEndpoint<CreateTodoRequestData, IAddTodoResponseInProgress>(
          new AddTodoRequestDataParser(
            new AddTodoDtoParser(
              new AddTodoDataParser(
                new RequiredStringParser(nameof(AddTodoDataDto.Title)),
                new RequiredStringParser(nameof(AddTodoDataDto.Content))
              ),
              new DictionaryParser<string, string>(nameof(AddTodoDto.Links))
            )),
          addTodoCommandFactory,
          new ResponseInProgressFactory()));
  }

  private static EndpointWithFallbackExceptionHandling StandardEndpoint(
    IEndpointsSupport support, 
    IRequestProcessingPolicy policy, 
    IAsyncEndpoint executingCommandEndpoint)
  {
    return new EndpointWithFallbackExceptionHandling(
      support,
      new EndpointWithRequestIdAsTraceId(
        new EndpointWithSupportScope(policy.InitialSupportScopeProperties(),
          support,
          new HttpRequestCompletenessValidatingEndpoint(policy.HttpRequestCompletenessCondition(),
            support,
            new EndpointWithSupportScope(policy.AdditionalPostValidationSupportScopeProperties(),
              support,
              new AuthorizationEndpoint(
                policy.TokenValidationParameters(),
                executingCommandEndpoint))))));
  }

  public IAsyncEndpoint LinkTodoEndpoint { get; }
  public IAsyncEndpoint AddTodoEndpoint { get; }
}

public class FromRequestScopePropertySet : ILoggedPropertySet
{
  private readonly IScopeProperty[] _scopeProperties;

  public FromRequestScopePropertySet(params IScopeProperty[] scopeProperties)
  {
    _scopeProperties = scopeProperties;
  }

  public Dictionary<string, object> ToDictionaryUsing(HttpRequest httpRequest)
  {
    return _scopeProperties.Select(p => p.ValueFrom(httpRequest))
      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
  }
}