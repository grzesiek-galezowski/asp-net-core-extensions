using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TodoApp.Bootstrap;

public class ServiceSupport : IEndpointsSupport
{
  private readonly ILoggerFactory _loggerFactory;

  public ServiceSupport(ILoggerFactory loggerFactory)
  {
    _loggerFactory = loggerFactory;
  }

  public IDisposable BeginScope(object source, string operationName)
  {
    var scope = _loggerFactory.CreateLogger(source.GetType())
      .BeginScope(new Dictionary<string, object>() //bug add serilog or nlog config
      {
        ["operationName"] = operationName
      });
    return scope;
  }
}