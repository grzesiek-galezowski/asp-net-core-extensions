using Microsoft.AspNetCore.Http;

namespace TodoApp.Http.HttpValidation;

public interface IHttpRequestCondition
{
  void Assert(HttpRequest request);
}