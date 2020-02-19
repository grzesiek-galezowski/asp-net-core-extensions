using System.Threading.Tasks;

namespace TodoApp.Logic.App
{
  public class LinkTodoCommand : ITodoCommand
  {
    private readonly TodoCreatedDto _dto1;
    private readonly TodoCreatedDto _dto2;

    public LinkTodoCommand(TodoCreatedDto dto1, TodoCreatedDto dto2)
    {
      _dto1 = dto1;
      _dto2 = dto2;
    }

    public Task ExecuteAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}