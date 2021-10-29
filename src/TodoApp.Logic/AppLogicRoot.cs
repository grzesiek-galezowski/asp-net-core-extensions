namespace TodoApp.Logic;

public class AppLogicRoot
{
  public TodoCommandFactory CommandFactory { get; }

  public AppLogicRoot(IUserTodosDao userTodosDao, IIdGenerator idGenerator)
  {
    CommandFactory = new TodoCommandFactory(
      idGenerator, 
      userTodosDao);
  }
}