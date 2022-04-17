using System;

namespace TodoApp.Logic;

public interface IIdSequence
{
  Guid Generate();
}