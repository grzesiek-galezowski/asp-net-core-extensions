using System.Threading;
using System.Threading.Tasks;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Logic.Users;

public class RegisterUserCommand : IAppCommand
{
  private readonly RegisterUserRequestData _createUserData;
  private readonly IUsersDao _usersDao;
  private readonly IIdGenerator _idGenerator;
  private readonly IRegisterUserResponseInProgress _responseInProgress;

  public RegisterUserCommand(RegisterUserRequestData createUserData,
    IUsersDao usersDao,
    IIdGenerator idGenerator,
    IRegisterUserResponseInProgress responseInProgress)
  {
    _createUserData = createUserData;
    _usersDao = usersDao;
    _idGenerator = idGenerator;
    _responseInProgress = responseInProgress;
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    //bug throw if user already exists
    await _usersDao.AddAsync(_idGenerator.Generate(), _createUserData, cancellationToken);
    await _responseInProgress.SuccessAsync(cancellationToken);

  }
}