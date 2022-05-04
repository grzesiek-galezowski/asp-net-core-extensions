using System.Net;
using Flurl.Http;

namespace TodoAppSpecification.HostSpecification.Automation;

public class LinkTodosResponse
{
  public readonly IFlurlResponse Response;

  public LinkTodosResponse(IFlurlResponse response)
  {
    Response = response;
  }

  public void ShouldBeSuccessful()
  {
    Response.ResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}