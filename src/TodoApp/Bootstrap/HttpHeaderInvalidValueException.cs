using Microsoft.Extensions.Primitives;

namespace TodoApp.Bootstrap;

public class HttpHeaderInvalidValueException : HttpRequestInvalidException
{
  public HttpHeaderInvalidValueException(string headerName, string expected, StringValues actual)
  : base($"Expected header {headerName} to have value {expected} but was {actual}")
  {
  }
}