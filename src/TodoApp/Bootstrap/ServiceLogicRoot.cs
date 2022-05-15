using System;
using Microsoft.IdentityModel.Tokens;
using NLog;
using TodoApp.Db;
using TodoApp.Http;
using TodoApp.Http.Flow;
using TodoApp.Logic;
using TodoApp.Random;
using TodoApp.Support;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace TodoApp.Bootstrap;

public class ServiceLogicRoot : IAsyncDisposable
{
  private readonly EndpointsAdapter _endpointsAdapter;
  private readonly StorageAdapter _storageAdapter;

  public ServiceLogicRoot(
    TokenValidationParameters tokenValidationParameters, 
    ILoggerFactory loggerFactory)
  {
    var storageAdapter = new StorageAdapter();
    var appLogicRoot = new AppLogicRoot(
      storageAdapter.UserTodosDao,
      new NewGuidBasedIdSequence());
    var endpointsAdapter = new EndpointsAdapter(
      appLogicRoot.TodoCommandFactory,
      appLogicRoot.TodoCommandFactory,
      tokenValidationParameters, 
      LoggingAdapter.CreateServiceSupport(loggerFactory));
    _endpointsAdapter = endpointsAdapter;
    _storageAdapter = new StorageAdapter();
  }

  public IAsyncEndpoint AddTodoEndpoint => _endpointsAdapter.AddTodoEndpoint;
  public IAsyncEndpoint LinkTodoEndpoint => _endpointsAdapter.LinkTodoEndpoint;

  public async ValueTask DisposeAsync()
  {
    _storageAdapter.Dispose();
    await Task.CompletedTask;
  }

  public static LogFactory CreateLogFactory()
  {
      return LoggingAdapter.CreateConsoleLogFactory();
  }
}