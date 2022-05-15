namespace TodoAppSpecification.HostSpecification.Automation;

public class AddTodoResponse
{
  private readonly IFlurlResponse _response;

  public AddTodoResponse(IFlurlResponse response)
  {
    _response = response;
  }

  public AddTodoResponse ShouldBeSuccessful()
  {
    _response.StatusCode.Should().Be(200);
    return this;
  }

  public async Task<Guid> GetTodoId() 
    => await _response.GetJsonAsync<Guid>();
}