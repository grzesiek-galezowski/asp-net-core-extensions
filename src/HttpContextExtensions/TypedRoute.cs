namespace HttpContextExtensions;

public class TypedRoute
{
  private readonly RouteValueDictionary _values;

  public TypedRoute(RouteValueDictionary values)
  {
    _values = values;
  }

  public delegate bool TryParse<TInput,TOutput>(TInput input, out TOutput output);

  public Guid Guid(string name) => Get<Guid>(name, System.Guid.TryParse);

  public T Get<T>(string name, TryParse<string?, T> tryParse)
  {
    var requestRouteValue = _values[name];

    if (requestRouteValue == null)
    {
      throw new HttpRouteElementMissingException(name);
    }

    if (!tryParse(requestRouteValue.ToString(), out var result))
    {
      throw new HttpRouteElementIsNotAValidValueOfTException<T>(name, requestRouteValue);
    }

    return result;
  }
}

public static class TypedRouteExtensions
{
  public static TypedRoute GetTypedRoute(this HttpRequest request) => new TypedRoute(request.RouteValues);
}