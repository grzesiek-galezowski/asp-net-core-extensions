namespace TddXt.HttpContextMock;

public static class HttpResponseMockAssertionsExtensions
{
  public static HttpResponseMockAssertions Should(this HttpResponseMock response)
  {
    return new HttpResponseMockAssertions(response);
  }
}