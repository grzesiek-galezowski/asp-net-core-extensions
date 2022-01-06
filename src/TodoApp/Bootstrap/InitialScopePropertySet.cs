using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

public class InitialScopePropertySet : ILoggedPropertySet
{
  private readonly string _operationName;

  public InitialScopePropertySet(string operationName)
  {
    _operationName = operationName;
  }

  public Dictionary<string, object> ToDictionaryUsing(HttpRequest httpRequest)
  {
    return new Dictionary<string, object>
    {
      ["operationName"] = _operationName,
      ["requestId"] = httpRequest.HttpContext.TraceIdentifier
    };
  }
}