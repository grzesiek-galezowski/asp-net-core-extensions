using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using TddXt.HttpContextMock;
using TodoApp;
using static TddXt.AnyRoot.Root;

namespace TodoAppSpecification
{
  public class AddTodoActionSpecification
  {
    [Test]
    public async Task ShouldRespondToPostTodoWithTodoCreated()
    {
      //GIVEN
      var idGenerator = Substitute.For<IIdGenerator>();
      var action = new AddTodoAction(idGenerator);
      var id = Any.String();
      var context = HttpContextMock.Default();
      var httpRequest = context.Request().PostJson(new TodoDto
      {
        Title = "a",
        Content = "b"
      }).RealInstance;
      var httpResponse = context.Response().RealInstance;
      var action = new AddTodoAction(
        httpRequest, 
        httpResponse, 
        idGenerator);

      idGenerator.Generate().Returns(id);

      await action.ExecuteAsync(
          context.Request().RealInstance, 
          context.Response().RealInstance);

      //THEN
      context.Response().Should().HaveBody(new TodoCreatedDto
      {
        Title = "a",
        Content = "b",
        Id = id
      });
    }
  }
}