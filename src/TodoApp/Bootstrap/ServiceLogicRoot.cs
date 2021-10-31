using System;
using System.IO;
using System.Threading.Tasks;
using LiteDB.Engine;
using TodoApp.Db;
using TodoApp.Http;
using TodoApp.Logic;
using TodoApp.Random;

namespace TodoApp.Bootstrap;

public class ServiceLogicRoot : IAsyncDisposable
{
  private readonly Stream _tempStream;
  private readonly EndpointsAdapter _endpointsAdapter;

  public ServiceLogicRoot()
  {
    _tempStream = new TempStream();

    var appLogicRoot = new AppLogicRoot(
      new UserTodosDao(_tempStream),
      new IdGenerator(), 
      new UsersDao(_tempStream));
    var endpointsAdapter = new EndpointsAdapter(
      appLogicRoot.TodoCommandFactory,
      appLogicRoot.TodoCommandFactory,
      appLogicRoot.UserCommandFactory,
      appLogicRoot.UserCommandFactory);
    _endpointsAdapter = endpointsAdapter;
  }

  public IAsyncEndpoint AddTodoEndpoint => _endpointsAdapter.AddTodoEndpoint;
  public IAsyncEndpoint LinkTodoEndpoint => _endpointsAdapter.LinkTodoEndpoint;
  public IAsyncEndpoint RegisterUserEndpoint => _endpointsAdapter.RegisterUserEndpoint;
  public IAsyncEndpoint LoginUserEndpoint => _endpointsAdapter.LoginUserEndpoint;

  public ValueTask DisposeAsync()
  {
    _tempStream.Dispose();
    return ValueTask.CompletedTask;
  }
}