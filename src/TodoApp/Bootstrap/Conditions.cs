namespace TodoApp.Bootstrap;

static internal class Conditions
{
  public static IHttpRequestCondition HeaderAsExpected(
    string headerName, 
    string expectedHeaderValue)
  {
    return AggregateCondition.ConsistingOf(
      new HeaderValueNotNullOrWhitespaceCondition(headerName), 
      new HeaderEqualToExpectedCondition(headerName, expectedHeaderValue));
  }

  public static HeaderValueNotNullOrWhitespaceCondition HeaderDefined(string headerName)
  {
    return new HeaderValueNotNullOrWhitespaceCondition(headerName);
  }

  public static IHttpRequestCondition QueryParamDefined(string paramName)
  {
    return new QueryParamNotNullOrWhitespaceCondition(paramName);
  }

  public static IHttpRequestCondition RouteContainsGuidNamed(string name)
  {
    return new RouteParamIsAValidGuid(name);
  }
}