using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TodoApp.Http.Support;

namespace TodoApp.Bootstrap;

public class ServiceSupport : IEndpointsSupport
{
  private readonly ILoggerFactory _loggerFactory;

  public ServiceSupport(ILoggerFactory loggerFactory)
  {
    _loggerFactory = loggerFactory;
  }

  //bug obsolete?
  public IDisposable BeginScope(object source, string operationName)
  {
    var dictionary = new Dictionary<string, object>()
    {
      ["operationName"] = operationName
    };
    return BeginScope(source, dictionary);
  }

  public IDisposable BeginScope(object source, Dictionary<string, object> properties)
  {
    return LoggerFor(source).BeginScope(properties);
  }

  public void BadRequest(object source, Exception exception)
  {
    LoggerFor(source).LogError(exception, "Request is invalid");
  }

  public void UnhandledException(object source, Exception exception)
  {
    LoggerFor(source).LogError(exception, "Unhandled exception");
  }

  public void AuthorizationFailed(object source, Exception exception)
  {
    LoggerFor(source).LogError(exception, "Authorization failed");
  }

  private ILogger LoggerFor(object source)
  {
    return _loggerFactory.CreateLogger(source.GetType());
  }
}