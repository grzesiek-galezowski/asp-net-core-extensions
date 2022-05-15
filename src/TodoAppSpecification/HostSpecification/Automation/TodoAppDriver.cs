using System.Net.Mime;

namespace TodoAppSpecification.HostSpecification.Automation;

public class TodoAppDriver : IAsyncDisposable
{
  private readonly AppFactory _appFactory;

  public static async Task<TodoAppDriver> CreateInstance()
  {
    var appFactory = new AppFactory();
    await appFactory.ReadinessCheck();
    return new TodoAppDriver(appFactory);
  }

  private TodoAppDriver(AppFactory appFactory)
  {
    _appFactory = appFactory;
  }

  public async ValueTask DisposeAsync()
  {
    await _appFactory.PrintLogs();
    await _appFactory.DisposeAsync();
  }

  public async Task<AddTodoResponse> AttemptToAddNewTodo(AddTodoDtoBuilder addTodoDtoBuilder)
  {
    var addTodoDto = addTodoDtoBuilder.Build();

    var response = await _appFactory.FlurlClient()
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
    var flurlResponse = await _appFactory.FlurlClient()
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