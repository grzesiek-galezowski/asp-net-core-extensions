using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Logic.Users;

public class UserCommandFactory : 
  ICommandFactory<RegisterUserRequestData, IRegisterUserResponseInProgress>, 
  ICommandFactory<LoginUserRequestData, ILoginUserResponseInProgress>
{
  private readonly IIdGenerator _idGenerator;
  private readonly IUsersDao _usersDao;

  public UserCommandFactory(IIdGenerator idGenerator, IUsersDao usersDao)
  {
    _idGenerator = idGenerator;
    _usersDao = usersDao;
  }

  public IAppCommand CreateCommand(RegisterUserRequestData dto, IRegisterUserResponseInProgress responseInProgress)
  {
    return new RegisterUserCommand(dto, _usersDao, _idGenerator, responseInProgress);
  }

  public IAppCommand CreateCommand(LoginUserRequestData dto, ILoginUserResponseInProgress responseInProgress)
  {
    return new LoginUserCommand(dto, _usersDao, responseInProgress);
  }
}