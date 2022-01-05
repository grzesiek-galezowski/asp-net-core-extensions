namespace TodoApp.Bootstrap;

public class HttpRouteElementMissingException : HttpRequestInvalidException
{
  public HttpRouteElementMissingException(string name)
    : base("trolololo") //bug
  {
  }
}