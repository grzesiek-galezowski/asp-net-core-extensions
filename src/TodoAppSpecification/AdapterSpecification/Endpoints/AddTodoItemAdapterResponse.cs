using TddXt.HttpContextMock;

namespace TodoAppSpecification.HostSpecification;

public class AddTodoItemAdapterResponse
{
  private readonly HttpResponseMock _httpResponseMock;

  public AddTodoItemAdapterResponse(HttpResponseMock httpResponseMock)
  {
    _httpResponseMock = httpResponseMock;
  }

  public void ShouldBeForbidden401()
  {
    StatusCodeShouldBe(401);
  }

  public void ShouldBe400BadRequest()
  {
    StatusCodeShouldBe(400);
  }

  private void StatusCodeShouldBe(int expected)
  {
    _httpResponseMock.RealInstance.StatusCode.Should().Be(expected);
  }
}