using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

public interface ILoggedPropertySet
{
  Dictionary<string, object> ToDictionaryUsing(HttpRequest httpRequest);
}