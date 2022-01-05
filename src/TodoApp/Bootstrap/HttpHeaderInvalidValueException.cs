using Microsoft.Extensions.Primitives;

namespace TodoApp.Bootstrap;

public class HttpHeaderInvalidValueException : HttpRequestInvalidException
{
  public HttpHeaderInvalidValueException(string headerName, string expected, StringValues actual)
  : base("Toto trolololo") //bug
  {
    //bug derive from base
  }
}