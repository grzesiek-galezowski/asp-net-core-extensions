using System;
using TodoApp.Logic.App;

namespace TodoApp
{
  public class IdGenerator : IIdGenerator
  {
    public string Generate()
    {
      return Guid.NewGuid().ToString();
    }
  }
}