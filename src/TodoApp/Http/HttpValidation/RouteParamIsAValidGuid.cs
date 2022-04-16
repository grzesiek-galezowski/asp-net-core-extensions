using HttpContextExtensions;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.HttpValidation;

public class RouteParamIsAValidGuid : IHttpRequestCondition
{
  private readonly string _name;

  public RouteParamIsAValidGuid(string name)
  {
    _name = name;
  }

  public void Assert(HttpRequest request)
  {
    request.GetTypedRoute().Guid(_name);
  }
}