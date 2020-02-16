using System.Collections.Generic;
using System.Linq;

namespace TddXt.HttpContextMock.Internal
{
  public static class HttpHeadersFromObject
  {
    internal static IEnumerable<HttpHeaderFromObject> ExtractHeaders(object properties)
    {
      return properties.GetType().GetProperties().Select(pi => new HttpHeaderFromObject(pi, properties));
    }
  }
}