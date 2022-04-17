using TodoApp.Logic.TodoNotes;

namespace TodoApp.Logic;

public class AppLogicRoot
{
  public TodoCommandFactory TodoCommandFactory { get; }

  public AppLogicRoot(IUserTodosDao userTodosDao, IIdSequence idSequence)
  {
    TodoCommandFactory = new TodoCommandFactory(
      idSequence, 
      userTodosDao);
  }
}