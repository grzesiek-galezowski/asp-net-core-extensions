using System;

namespace TodoApp.Bootstrap;

public class HttpRequestInvalidException : Exception
{
  public HttpRequestInvalidException(string? message) : base(message)
  {
  }

  public HttpRequestInvalidException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}