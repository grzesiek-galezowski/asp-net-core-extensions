using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.HttpValidation;

internal class HeaderValueNotNullOrWhitespaceCondition : IHttpRequestCondition
{
  private readonly string _headerName;

  public HeaderValueNotNullOrWhitespaceCondition(string headerName)
  {
    _headerName = headerName;
  }

  public void Assert(HttpRequest request)
  {
    //bug split null and whitespace and throw better exceptions
    if (string.IsNullOrWhiteSpace(request.Headers[_headerName]))
    {
      throw new HttpHeaderMissingException(_headerName);
    }
  }
}