using System;

namespace TodoApp.Db;

//bug write a scenario about what happens in the app logic?
public class NoteNotFoundException : Exception
{
  public NoteNotFoundException(Guid id, Exception exception)
  : base($"Could not find a note with an id {id}", exception)
  {
    
  }
}