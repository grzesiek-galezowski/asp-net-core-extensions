using System.Net.Mime;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using TodoApp.Http.Flow;
using TodoApp.Http.HttpValidation;
using TodoApp.Http.Support;

namespace TodoApp.Http.LinkTodos;

public class LinkTodoItemsRequestProcessingPolicy : IRequestProcessingPolicy
{
  private readonly TokenValidationParameters _validationParameters;

  public LinkTodoItemsRequestProcessingPolicy(TokenValidationParameters tokenValidationParameters)
  {
    _validationParameters = tokenValidationParameters;
  }

  public FromRequestScopePropertySet InitialSupportScopeProperties()
  {
    return new FromRequestScopePropertySet(
      ScopeProperty.FromConstant("operationName", "Add a TODO item"),
      ScopeProperty.TraceIdentifierAs("requestId"));
  }

  public IHttpRequestCondition HttpRequestCompletenessCondition()
  {
    return AggregateCondition.ConsistingOf(
      Conditions.HeaderAsExpected(HeaderNames.Accept, MediaTypeNames.Application.Json),
      Conditions.HeaderAsExpected(HeaderNames.ContentType, MediaTypeNames.Application.Json),
      Conditions.HeaderDefined(HeaderNames.Authorization),
      Conditions.QueryParamDefined(HttpRequestParameterNames.CustomerId)
    );
  }

  public FromRequestScopePropertySet AdditionalPostValidationSupportScopeProperties()
  {
    return new FromRequestScopePropertySet(
      ScopeProperty.FromQuery(HttpRequestParameterNames.CustomerId));
  }

  public TokenValidationParameters TokenValidationParameters()
  {
    return _validationParameters;
  }
}