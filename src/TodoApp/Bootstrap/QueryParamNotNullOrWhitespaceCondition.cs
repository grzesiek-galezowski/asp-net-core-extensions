using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

internal class QueryParamNotNullOrWhitespaceCondition : IHttpRequestCondition
{
  private readonly string _paramName;

  public QueryParamNotNullOrWhitespaceCondition(string paramName)
  {
    _paramName = paramName;
  }

  public void Assert(HttpRequest request)
  {
    if(string.IsNullOrWhiteSpace(request.Query[_paramName].ToString())) //bug split null and whitespace
    {
      throw new HttpQueryParameterMissingException(_paramName, request.QueryString);
    }
  }
}