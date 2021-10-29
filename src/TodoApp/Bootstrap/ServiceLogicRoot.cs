using System;
using System.Threading.Tasks;
using LiteDB.Engine;
using TodoApp.Db;
using TodoApp.Http;
using TodoApp.Logic;
using TodoApp.Random;

namespace TodoApp.Bootstrap;

public class ServiceLogicRoot : IAsyncDisposable
{
  private readonly TempStream _tempStream;
  private readonly EndpointsAdapter _endpointsAdapter;

  public ServiceLogicRoot()
  {
    _tempStream = new TempStream();

    var appLogicRoot = new AppLogicRoot(
      new UserTodosDao(_tempStream),
      new IdGenerator());
    var endpointsAdapter = new EndpointsAdapter(
      appLogicRoot.CommandFactory,
      appLogicRoot.CommandFactory);
    _endpointsAdapter = endpointsAdapter;
  }

  public IAsyncEndpoint AddTodoEndpoint => _endpointsAdapter.AddTodoEndpoint;
  public IAsyncEndpoint LinkTodoEndpoint => _endpointsAdapter.LinkTodoEndpoint;

  public ValueTask DisposeAsync()
  {
    _tempStream.Dispose();
    return ValueTask.CompletedTask;
  }
}