using System;
using System.IO;
using System.Threading.Tasks;
using LiteDB.Engine;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Targets;
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
  private readonly Stream _databaseStream;
  private readonly EndpointsAdapter _endpointsAdapter;

  public ServiceLogicRoot(
    TokenValidationParameters tokenValidationParameters, 
    ILoggerFactory loggerFactory)
  {
    _databaseStream = new TempStream();
    var appLogicRoot = new AppLogicRoot(
      new UserTodosDao(_databaseStream),
      new IdGenerator());
    var endpointsAdapter = new EndpointsAdapter(
      appLogicRoot.TodoCommandFactory,
      appLogicRoot.TodoCommandFactory,
      tokenValidationParameters, 
      new ServiceSupport(loggerFactory));
    _endpointsAdapter = endpointsAdapter;
  }

  public IAsyncEndpoint AddTodoEndpoint => _endpointsAdapter.AddTodoEndpoint;
  public IAsyncEndpoint LinkTodoEndpoint => _endpointsAdapter.LinkTodoEndpoint;

  public ValueTask DisposeAsync()
  {
    _databaseStream.Dispose();
    return ValueTask.CompletedTask;
  }

  public static LogFactory CreateLogFactory()
  {
      //bug logging adapter?
      return ConfigForLogger.CreateLogFactory(new ColoredConsoleTarget("coloredConsole"));
  }
}