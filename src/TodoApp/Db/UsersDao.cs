using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Maybe;
using LiteDB;
using NullableReferenceTypesExtensions;
using TodoApp.Logic.TodoNotes.LinkTodos;
using TodoApp.Logic.Users;

namespace TodoApp.Db;

public class UsersDao : IUsersDao
{
  private readonly Stream _stream;

  public UsersDao(Stream stream)
  {
    _stream = stream;
  }

  public async Task AddAsync(Guid id, RegisterUserRequestData createUserData, CancellationToken cancellationToken)
  {
    using var liteDb = new LiteDatabase(_stream);
    liteDb.GetCollection<PersistentUserDto>().Insert(
      new PersistentUserDto
      {
        Id = id,
        Login = createUserData.Login,
        Password = createUserData.Password,
      });
  }

  public async Task<Maybe<Guid>> FindUserByLoginAndPasswordAsync(string requestedLogin, string requestedPassword,
    CancellationToken cancellationToken)
  {
    using var liteDb = new LiteDatabase(_stream);
    return liteDb.GetCollection<PersistentUserDto>().FindAll()
      .SingleMaybe(u => u.Login == requestedLogin && u.Password == requestedPassword)
      .Select(u => u.Id);
  }
}