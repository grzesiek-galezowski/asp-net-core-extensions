using System;
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
    var requestRouteValue = request.RouteValues[_name];

    if (requestRouteValue == null)
    {
      throw new HttpRouteElementMissingException(_name);
    } //bug separate assertion?

    if (!Guid.TryParse(requestRouteValue.ToString(), out _))
    {
      throw new HttpRouteElementIsNotAValidGuidException(_name, requestRouteValue);
    }
  }
}