using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.HttpValidation;

public class AggregateCondition : IHttpRequestCondition
{
  private readonly IHttpRequestCondition[] _conditions;

  public AggregateCondition(params IHttpRequestCondition[] conditions)
  {
    _conditions = conditions;
  }

  public static IHttpRequestCondition ConsistingOf(params IHttpRequestCondition[] conditions)
  {
    return new AggregateCondition(conditions);
  }

  public void Assert(HttpRequest request)
  {
    foreach (var condition in _conditions)
    {
      condition.Assert(request);
    }
  }
}