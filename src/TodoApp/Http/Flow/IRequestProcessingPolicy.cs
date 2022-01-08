using Microsoft.IdentityModel.Tokens;
using TodoApp.Http.HttpValidation;

namespace TodoApp.Http.Flow;

public interface IRequestProcessingPolicy
{
  TokenValidationParameters TokenValidationParameters();
  FromRequestScopePropertySet AdditionalPostValidationSupportScopeProperties();
  IHttpRequestCondition HttpRequestCompletenessCondition();
  FromRequestScopePropertySet InitialSupportScopeProperties();
}