namespace HttpContextExtensions;

public class HttpRouteElementMissingException : HttpRequestInvalidException
{
  public HttpRouteElementMissingException(string routeElement)
    : base($"Expected route element {routeElement} not found")
  {
  }
}