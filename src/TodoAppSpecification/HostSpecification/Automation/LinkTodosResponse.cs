using System.Net;

namespace TodoAppSpecification.HostSpecification.Automation;

public class LinkTodosResponse
{
  private readonly IFlurlResponse _response;

  public LinkTodosResponse(IFlurlResponse response)
  {
    _response = response;
  }

  public void ShouldBeSuccessful()
  {
    _response.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}