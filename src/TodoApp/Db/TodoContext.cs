using Microsoft.EntityFrameworkCore;

namespace TodoApp.Db;

public class TodoContext : DbContext
{
  public DbSet<PersistentTodoDto> Todos { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseInMemoryDatabase("Todos");
  }
}