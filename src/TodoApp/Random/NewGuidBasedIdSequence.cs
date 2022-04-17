using System;
using TodoApp.Logic;

namespace TodoApp.Random;

public class NewGuidBasedIdSequence : IIdSequence
{
  public Guid Generate()
  {
    return Guid.NewGuid();
  }
}