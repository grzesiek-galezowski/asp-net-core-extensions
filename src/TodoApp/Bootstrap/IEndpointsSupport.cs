using System;

namespace TodoApp.Bootstrap;

public interface IEndpointsSupport
{
  IDisposable BeginScope(object source, string operationName);
}