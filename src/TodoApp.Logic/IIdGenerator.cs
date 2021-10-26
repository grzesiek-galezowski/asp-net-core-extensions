using System;

namespace TodoApp.Logic;

public interface IIdGenerator
{
  Guid Generate();
}