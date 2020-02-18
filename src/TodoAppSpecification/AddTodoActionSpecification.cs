using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using TddXt.HttpContextMock;
using TodoApp;
using TodoApp.App;
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
      var action = new AddTodoAction<TodoDto, IAddTodoResponseInProgress>(
        new RequestParser<TodoDto>(),
        new TodoCommandFactory(idGenerator, new TodoRepository()),
        new ResponseInProgressFactory());
      var id = Any.String();
      var context = HttpContextMock.Default();
      var httpRequest = context.Request().PostJson(new TodoDto
      {
        Title = "a",
        Content = "b"
      }).RealInstance;
      var httpResponse = context.Response().RealInstance;

      idGenerator.Generate().Returns(id);

      await action.ExecuteAsync(
          httpRequest, 
          httpResponse);

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