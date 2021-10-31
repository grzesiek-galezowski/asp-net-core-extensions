using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Maybe;
using TodoApp.Logic.TodoNotes.LinkTodos;

namespace TodoApp.Logic.Users;

public interface IUsersDao
{
  Task AddAsync(Guid id, RegisterUserRequestData createUserData, CancellationToken cancellationToken);
  Task<Maybe<Guid>> FindUserByLoginAndPasswordAsync(string requestedLogin, string requestedPassword,
    CancellationToken cancellationToken);
}