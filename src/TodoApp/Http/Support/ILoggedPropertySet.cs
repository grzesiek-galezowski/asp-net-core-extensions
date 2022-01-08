using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.Support;

public interface ILoggedPropertySet
{
  Dictionary<string, object> ToDictionaryUsing(HttpRequest httpRequest);
}