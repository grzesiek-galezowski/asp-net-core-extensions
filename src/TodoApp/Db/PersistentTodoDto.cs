using System;

namespace TodoApp.Db;

public record PersistentTodoDto
{
  public Guid? Id { get; init; }
  public string? Title { get; init; }
  public string? Content { get; init; }
  public Guid[]? LinkedNotes { get; init; }
}