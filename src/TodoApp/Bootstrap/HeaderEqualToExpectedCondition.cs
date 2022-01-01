using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

internal class HeaderEqualToExpectedCondition : IHttpRequestCondition
{
  private readonly string _headerName;
  private readonly string _expectedValue;

  public HeaderEqualToExpectedCondition(string headerName, string expectedValue)
  {
    _headerName = headerName;
    _expectedValue = expectedValue;
  }

  public void Assert(HttpRequest request)
  {
    if (request.Headers[_headerName] != _expectedValue)
    {
      throw new HttpHeaderInvalidValueException(_headerName, _expectedValue,
        request.Headers[_headerName]);
    }
  }
}