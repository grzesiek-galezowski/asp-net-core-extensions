using System.Net.Mime;
using Flurl.Http;
using TodoAppSpecification.HostSpecification.Automation;

namespace TodoAppSpecification.HostSpecification;

//bug wait for healthchecks

public class TodoAppDriver : IAsyncDisposable
{
  public TodoAppDriver()
  {
    AppFactory = new AppFactory();
  }

  public readonly AppFactory AppFactory;

  public FlurlClient FlurlClient()
  {
    return new FlurlClient(AppFactory.CreateClient());
  }

  public async ValueTask DisposeAsync()
  {
    await AppFactory.DisposeAsync();
  }

  public async Task<AddTodoResponse> AttemptToAddNewTodo(AddTodoDtoBuilder addTodoDtoBuilder)
  {
    var addTodoDto = addTodoDtoBuilder.Build();

    var response = await FlurlClient()
      .Request("/todo")
      .WithHeader("Authorization", "Bearer " + TestTokens.GenerateToken())
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .AllowAnyHttpStatus()
      .PostJsonAsync(addTodoDto);
    return new AddTodoResponse(response);
  }

  public async Task<AddTodoResponse> AddNewTodo(AddTodoDtoBuilder addTodoDtoBuilder)
  {
    return (await AttemptToAddNewTodo(addTodoDtoBuilder)).ShouldBeSuccessful();
  }

  public async Task<LinkTodosResponse> AttemptToLinkTodos(Guid id1, Guid id2)
  {
    var flurlResponse = await FlurlClient()
      .Request($"/todo/{id1}/link/{id2}")
      .WithHeader("Authorization", $"Bearer {TestTokens.GenerateToken()}")
      .WithHeader("Accept", MediaTypeNames.Application.Json)
      .WithHeader("Content-Type", MediaTypeNames.Application.Json)
      .SetQueryParam("customerId", Any.String())
      .AllowAnyHttpStatus()
      .PostAsync();
    return new LinkTodosResponse(flurlResponse);
  }
}

public class ManagingTodosSpecification
{
  [Test]
  public async Task ShouldBeAbleToCreateATodo()
  {
    //GIVEN
    await using var driver = new TodoAppDriver();

    //WHEN
    var addTodoResponse = await driver.AttemptToAddNewTodo(AddTodoDtoBuilder.UniqueTodo());

    //THEN
    addTodoResponse.ShouldBeSuccessful();
  }

  [Test]
  public async Task ShouldBeAbleToLinkTodos()
  {
    //GIVEN
    await using var driver = new TodoAppDriver();

    var addTodo1Response = await driver.AddNewTodo(AddTodoDtoBuilder.UniqueTodo());
    var addTodo2Response = await driver.AddNewTodo(AddTodoDtoBuilder.UniqueTodo());

    var id1 = await addTodo1Response.GetTodoId();
    var id2 = await addTodo2Response.GetTodoId();

    //WHEN
    var linkResponse = await driver.AttemptToLinkTodos(id1, id2);

    //THEN
    linkResponse.ShouldBeSuccessful();
    //bug This is not enough. Why do we link the TODOs?
  }

}