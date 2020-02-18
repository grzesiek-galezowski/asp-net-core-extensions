using System.Threading.Tasks;

namespace TodoApp.App
{
  public interface ITodoCommand
  {
    Task ExecuteAsync();
  }
}