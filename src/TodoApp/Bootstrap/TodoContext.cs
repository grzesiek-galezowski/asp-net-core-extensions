using Microsoft.EntityFrameworkCore;
using TodoApp.Logic.App;

namespace TodoApp.Bootstrap
{
  public class TodoContext : DbContext
  {
    public DbSet<TodoCreatedDto> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseInMemoryDatabase("Todos");
    }
  }
}