using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using TddXt.HttpContextMock;
using TodoApp;
using TodoApp.Logic.App;
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
      var action = new ExecuteCommandAction<TodoDto, IAddTodoResponseInProgress>(
        new RequestParser(),
        new TodoCommandFactory(idGenerator, new UserTodos()),
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
          httpResponse,
          Any.Instance<CancellationToken>());

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