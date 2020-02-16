using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TddXt.HttpContextMock;
using TodoApp;

namespace TodoAppSpecification
{
  public class Tests
  {
    [Test]
    public async Task Test1()
    {
      var context = new HttpContextMock();
      var idGenerator = Substitute.For<IIdGenerator>();
      var action = new AddTodoAction(context.Request().RealInstance, context.Response().RealInstance, idGenerator);
      string id = "123";

      idGenerator.Generate().Returns(id);

      context.Request().PostJson(new TodoDto()
      {
        Title = "a",
        Content = "b"
      });

      await action.ExecuteAsync();

      context.Response().Should().HaveBody(new TodoCreatedDto()
      {
        Title = "a",
        Content = "b",
        Id = id
      });
    }
  }
}