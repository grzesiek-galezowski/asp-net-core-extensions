using System;
using TodoApp.Logic;

namespace TodoApp.Random;

public class IdGenerator : IIdGenerator
{
  public Guid Generate()
  {
    return Guid.NewGuid();
  }
}