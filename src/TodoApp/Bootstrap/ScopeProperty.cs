using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

public interface IScopeProperty
{
  KeyValuePair<string, object> ValueFrom(HttpRequest httpRequest);
}

public class ScopeProperty : IScopeProperty
{
  private readonly string _paramName;
  private readonly Func<HttpRequest, object> _extractValue;

  private ScopeProperty(string paramName, Func<HttpRequest, object> extractValue)
  {
    _paramName = paramName;
    _extractValue = extractValue;
  }

  public static IScopeProperty FromQuery(string paramName)
  {
    return new ScopeProperty(paramName, r => r.Query[paramName].ToString());
  }

  public KeyValuePair<string, object> ValueFrom(HttpRequest httpRequest)
  {
    return KeyValuePair.Create(_paramName, _extractValue(httpRequest));
  }
}
