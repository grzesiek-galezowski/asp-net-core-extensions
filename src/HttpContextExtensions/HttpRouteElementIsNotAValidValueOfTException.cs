namespace HttpContextExtensions;

public class HttpRouteElementIsNotAValidValueOfTException<T> : HttpRequestInvalidException
{
  public HttpRouteElementIsNotAValidValueOfTException(string name, object requestRouteValue)
    : base("lolki dwa") //bug
  {
    
  }
}