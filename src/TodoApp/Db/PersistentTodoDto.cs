using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Db;

public class PersistentTodoDto
{
  [Key]
  public Guid? Id { get; init; }
  public string? Title { get; init; }
  public string? Content { get; init; }
  public PersistentTodoDto[]? LinkedNotes { get; init; }
}