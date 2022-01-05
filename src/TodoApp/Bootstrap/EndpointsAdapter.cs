using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using TodoApp.Http;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.AddTodo;
using TodoApp.Logic.TodoNotes.LinkTodos;
using static TodoApp.Bootstrap.HttpRequestParameterNames;

namespace TodoApp.Bootstrap;

public class EndpointsAdapter
{
  public EndpointsAdapter(
    ICommandFactory<CreateTodoRequestData, IAddTodoResponseInProgress> addTodoCommandFactory,
    ICommandFactory<LinkTodosRequestData, ILinkTodoResponseInProgress> linkTodoCommandFactory,
    TokenValidationParameters tokenValidationParameters, 
    IEndpointsSupport support)
  {
    //bug exception handling endpoint
    //bug endpoint logging a generated request id
    LinkTodoEndpoint =
      new EndpointWithSupportScope(
        new InitialScopePropertySet("Link TODO items"),
        support,
        new HttpRequestCompletenessValidatingEndpoint(
          AggregateCondition.ConsistingOf(
            Conditions.HeaderAsExpected(HeaderNames.Accept, MediaTypeNames.Application.Json),
            Conditions.HeaderAsExpected(HeaderNames.ContentType, MediaTypeNames.Application.Json),
            Conditions.RouteContainsGuidNamed(Id1),
            Conditions.RouteContainsGuidNamed(Id2),
            Conditions.HeaderDefined(HeaderNames.Authorization),
            Conditions.QueryParamDefined(CustomerId)),
          support,
          new EndpointWithSupportScope(
            new FromRequestScopePropertySet(
              ScopeProperty.FromQuery(CustomerId),
              ScopeProperty.FromRoute(Id1),
              ScopeProperty.FromRoute(Id2)),
            support,
            new AuthorizationEndpoint(
              tokenValidationParameters,
              new ExecutingCommandEndpoint<LinkTodosRequestData, ILinkTodoResponseInProgress>(
                new LinkTodosRequestDataParser(),
                linkTodoCommandFactory,
                new ResponseInProgressFactory())))));

    //bug exception handling endpoint
    //bug endpoint logging a generated request id
    //bug request validation endpoint
    //bug EndpointWithSupportScope should allow custom "value provider"
    AddTodoEndpoint =
      new EndpointWithSupportScope(new InitialScopePropertySet("Add a TODO item"),
        support,
        new AuthorizationEndpoint(
          tokenValidationParameters,
          new ExecutingCommandEndpoint<CreateTodoRequestData, IAddTodoResponseInProgress>(
            new AddTodoRequestDataParser(),
            addTodoCommandFactory,
            new ResponseInProgressFactory())));
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