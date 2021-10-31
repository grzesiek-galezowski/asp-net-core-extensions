using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.Users;

namespace TodoApp.Logic;

public class AppLogicRoot
{
  public TodoCommandFactory TodoCommandFactory { get; }
  public UserCommandFactory UserCommandFactory { get; }

  public AppLogicRoot(IUserTodosDao userTodosDao, IIdGenerator idGenerator, IUsersDao usersDao)
  {
    TodoCommandFactory = new TodoCommandFactory(
      idGenerator, 
      userTodosDao);
    UserCommandFactory = new UserCommandFactory(
      idGenerator,
      usersDao);
  }
}