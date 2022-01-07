using System;
using System.Collections.Generic;

namespace TodoApp.Bootstrap;

public interface IEndpointsSupport
{
  IDisposable BeginScope(object source, string operationName);
  IDisposable BeginScope(object source, Dictionary<string, object> properties);
  void BadRequest(object source, Exception exception);
  void UnhandledException(object source, Exception exception);
}