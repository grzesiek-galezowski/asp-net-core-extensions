using System.Threading;
using System.Threading.Tasks;
using TodoApp.Logic.TodoNotes;

namespace TodoApp.Logic.Users;

public class LoginUserCommand : IAppCommand
{
  private readonly LoginUserRequestData _dto;
  private readonly IUsersDao _usersDao;
  private readonly ILoginUserResponseInProgress _responseInProgress;

  public LoginUserCommand(LoginUserRequestData dto, IUsersDao usersDao, ILoginUserResponseInProgress responseInProgress)
  {
    _dto = dto;
    _usersDao = usersDao;
    _responseInProgress = responseInProgress;
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    var userId = await _usersDao.FindUserByLoginAndPasswordAsync(_dto.Login, _dto.Password, cancellationToken);
    
    if (userId.HasValue)
    {
      await _responseInProgress.SuccessAsync(userId.Value(), cancellationToken);
    }
    else
    {
      await _responseInProgress.SorryAsync(cancellationToken);
    }
  }
}