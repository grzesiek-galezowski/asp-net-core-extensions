using HttpContextExtensions;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.HttpValidation;

internal class HttpQueryParameterMissingException : HttpRequestInvalidException
{
  public HttpQueryParameterMissingException(string paramName, QueryString requestQueryString)
  : base($"Expected {paramName} query param name, but found none. Request query path: {requestQueryString.Value ?? string.Empty}")
  {
  }
}