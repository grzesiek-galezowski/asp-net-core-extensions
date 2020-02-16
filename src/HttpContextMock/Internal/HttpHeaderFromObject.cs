using System.Net.Http;
using System.Reflection;

namespace TddXt.HttpContextMock.Internal
{
  internal class HttpHeaderFromObject
  {
    private readonly PropertyInfo _value;
    private readonly object _properties;

    public HttpHeaderFromObject(PropertyInfo pi, object properties)
    {
      _value = pi;
      _properties = properties;
    }

    public string Name()
    {
      return _value.Name.Replace("_", "-");
    }

    public string Value()
    {
      return _value.GetValue(_properties).ToString();
    }
  }
}