using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TodoApp.Http.Support;

namespace TodoApp.Http;

public class FromRequestScopePropertySet : ILoggedPropertySet
{
  private readonly IScopeProperty[] _scopeProperties;

  public FromRequestScopePropertySet(params IScopeProperty[] scopeProperties)
  {
    _scopeProperties = scopeProperties;
  }

  public Dictionary<string, object> ToDictionaryUsing(HttpRequest httpRequest)
  {
    return _scopeProperties.Select(p => p.ValueFrom(httpRequest))
      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
  }
}