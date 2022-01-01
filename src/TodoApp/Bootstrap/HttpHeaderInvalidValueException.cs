using System;
using Microsoft.Extensions.Primitives;

namespace TodoApp.Bootstrap;

public class HttpHeaderInvalidValueException : Exception
{
  public HttpHeaderInvalidValueException(string headerName, string expected, StringValues actual)
  {
    //bug derive from base
  }
}