using System;

namespace TodoApp.Bootstrap;

public class HttpHeaderMissingException : Exception //bug consider common exception
{
  public HttpHeaderMissingException(string headerName)
  {
    //bug derive from base
  }
}