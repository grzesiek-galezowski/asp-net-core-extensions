using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.HttpContextMock;
using TodoApp.Db;
using TodoApp.Http;
using TodoApp.Logic;
using static TddXt.AnyRoot.Root;

namespace TodoAppSpecification;

public class AddTodoActionSpecification
{
  [Test]
  public async Task ShouldRespondToPostTodoWithTodoCreated()
  {
    //GIVEN
    var idGenerator = Substitute.For<IIdGenerator>();
    var action = new ExecutingCommandEndpoint<CreateTodoRequestData, IAddTodoResponseInProgress>(
      new JsonDocumentBasedRequestParser(),
      new TodoCommandFactory(idGenerator, new UserTodosDao()),
      new ResponseInProgressFactory());
    var id = Any.Guid();
    var context = HttpContextMock.Default();
    var httpRequest = context.Request().PostJson(new 
    {
      title = "a",
      content = "b"
    }).RealInstance;
    var httpResponse = context.Response().RealInstance;

    idGenerator.Generate().Returns(id);

    await action.ExecuteAsync(
      httpRequest, 
      httpResponse,
      new CancellationToken());

    //THEN
    context.Response().Should().HaveBody(
      new TodoCreatedData(id, "a", "b", ImmutableHashSet<Guid>.Empty));
  }
}