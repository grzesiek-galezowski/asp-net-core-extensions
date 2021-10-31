using System;

namespace TodoApp.Db;

public class PersistentUserDto
{
  public string? Login { get; set; }
  public string? Password { get; set; } //bug insecure
  public Guid Id { get; set; }
}