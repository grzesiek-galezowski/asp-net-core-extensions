using System;

namespace TodoApp.Bootstrap;

public class HttpHeaderMissingException : HttpRequestInvalidException //bug consider common exception
{
  public HttpHeaderMissingException(string headerName)
  : base("trolololo todo") //bug todo
  {
    //bug derive from base
  }
}