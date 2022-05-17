namespace HttpContextExtensions;

public class HttpRouteElementIsNotAValidValueOfTException<T> : HttpRequestInvalidException
{
  public HttpRouteElementIsNotAValidValueOfTException(string name, object requestRouteValue)
    : base($"Route element {name} with value {requestRouteValue} is not a valid value of type {typeof(T)}")
  {
    
  }
}