using System;
using System.Collections.Immutable;

namespace TodoApp.Logic.TodoNotes;

public record TodoCreatedData(
  Guid Id, 
  string Title, 
  string Content, 
  ImmutableHashSet<Guid> Links);