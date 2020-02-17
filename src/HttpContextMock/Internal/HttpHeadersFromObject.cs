using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace TddXt.HttpContextMock.Internal
{
  public static class HttpHeadersFromObject
  {
    internal static IEnumerable<HttpHeaderFromObject> ExtractHeaders(object properties)
    {
      return properties.GetType().GetProperties().Select(pi => new HttpHeaderFromObject(pi, properties));
    }

    internal static IEnumerable<KeyValuePair<string, StringValues>> ExtractHeadersKeyValuePairs(object o)
    {
      return ExtractHeaders(o).Select(h => 
        new KeyValuePair<string, StringValues>(h.Name(), h.Value()));
    }
  }
}