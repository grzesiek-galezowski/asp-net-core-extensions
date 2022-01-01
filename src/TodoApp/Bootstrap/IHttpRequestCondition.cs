using Microsoft.AspNetCore.Http;

namespace TodoApp.Bootstrap;

public interface IHttpRequestCondition
{
  void Assert(HttpRequest request);
}