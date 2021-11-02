using TodoApp.Logic.TodoNotes;

namespace TodoApp.Logic;

public class AppLogicRoot
{
  public TodoCommandFactory TodoCommandFactory { get; }

  public AppLogicRoot(IUserTodosDao userTodosDao, IIdGenerator idGenerator)
  {
    TodoCommandFactory = new TodoCommandFactory(
      idGenerator, 
      userTodosDao);
  }
}