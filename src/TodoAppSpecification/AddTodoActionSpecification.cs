using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TddXt.HttpContextMock;
using TodoApp.Bootstrap;

namespace TodoAppSpecification;

public class AddTodoActionSpecification
{
  [Test]
  public async Task ShouldRespondToPostTodoWithTodoCreated()
  {
    //GIVEN
    await using var serviceLogicRoot = new ServiceLogicRoot();
    var context = HttpContextMock.Default();
    var dto = new 
    {
      title = "a",
      content = "b"
    };
    var httpRequest = context.Request()
      .WithHeader("Authorization", "Bearer trolololo")
      .PostJson(dto)
      .RealInstance;
    var httpResponse = context.Response().RealInstance;

    await serviceLogicRoot.AddTodoEndpoint.HandleAsync(
      httpRequest, 
      httpResponse,
      new CancellationToken());

    //THEN
    context.Response().Should().HaveBody(dto);
  }
}