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
    public async Task ShouldRespondToPostTodo()
    {
      var context = HttpContextMock.Default();
      var idGenerator = Substitute.For<IIdGenerator>();
      var action = new AddTodoAction(idGenerator);
      var id = Any.String();

      idGenerator.Generate().Returns(id);

      context.Request().PostJson(new TodoDto
      {
        Title = "a",
        Content = "b"
      });

      await action.ExecuteAsync(
          context.Request().RealInstance, 
          context.Response().RealInstance);

      context.Response().Should().HaveBody(new TodoCreatedDto
      {
        Title = "a",
        Content = "b",
        Id = id
      });
    }
  }
}