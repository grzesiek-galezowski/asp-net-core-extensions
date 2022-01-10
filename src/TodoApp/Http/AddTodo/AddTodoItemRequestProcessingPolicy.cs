using System.Net.Mime;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using TodoApp.Http.Flow;
using TodoApp.Http.HttpValidation;
using TodoApp.Http.Support;

namespace TodoApp.Http.AddTodo;

public class AddTodoItemRequestProcessingPolicy : IRequestProcessingPolicy
{
  private readonly TokenValidationParameters _validationParameters;

  public TokenValidationParameters TokenValidationParameters()
  {
    return _validationParameters;
  }

  public AddTodoItemRequestProcessingPolicy(TokenValidationParameters tokenValidationParameters)
  {
    _validationParameters = tokenValidationParameters;
  }

  public FromRequestScopePropertySet AdditionalPostValidationSupportScopeProperties()
  {
    return new FromRequestScopePropertySet(
      ScopeProperty.FromQuery(HttpRequestParameterNames.CustomerId),
      ScopeProperty.FromRoute(HttpRequestParameterNames.Id1),
      ScopeProperty.FromRoute(HttpRequestParameterNames.Id2));
  }

  public IHttpRequestCondition HttpRequestCompletenessCondition()
  {
    return AggregateCondition.ConsistingOf(
      Conditions.HeaderAsExpected(HeaderNames.Accept, MediaTypeNames.Application.Json),
      Conditions.HeaderAsExpected(HeaderNames.ContentType, MediaTypeNames.Application.Json),
      Conditions.HeaderDefined(HeaderNames.Authorization),
      Conditions.QueryParamDefined(HttpRequestParameterNames.CustomerId),
      Conditions.RouteContainsGuidNamed(HttpRequestParameterNames.Id1),
      Conditions.RouteContainsGuidNamed(HttpRequestParameterNames.Id2));
  }

  public FromRequestScopePropertySet InitialSupportScopeProperties()
  {
    return new FromRequestScopePropertySet(
      ScopeProperty.FromConstant("operationName", "Link TODO items"),
      ScopeProperty.TraceIdentifierAs("requestId"));
  }
}