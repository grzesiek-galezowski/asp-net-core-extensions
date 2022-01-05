using System;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

internal class HttpQueryParameterMissingException : HttpRequestInvalidException
{
  public HttpQueryParameterMissingException(string paramName, QueryString requestQueryString)
  : base($"Expected {paramName} query param name, but found none. Request query path: {requestQueryString.Value ?? string.Empty}")
  {
  }
}