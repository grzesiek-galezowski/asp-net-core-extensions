namespace TodoApp.Bootstrap;

public class HttpRouteElementIsNotAValidGuidException : HttpRequestInvalidException
{
  public HttpRouteElementIsNotAValidGuidException(string name, object requestRouteValue)
    : base("lolki dwa") //bug
  {
    
  }
}