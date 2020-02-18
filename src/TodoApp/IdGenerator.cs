using System;
using TodoApp.App;

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