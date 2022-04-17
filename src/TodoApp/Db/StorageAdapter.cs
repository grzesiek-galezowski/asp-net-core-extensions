using System;
using System.IO;
using LiteDB.Engine;

namespace TodoApp.Db;

public class StorageAdapter : IDisposable
{
  private readonly Stream _databaseStream;

  public StorageAdapter()
  {
    _databaseStream = new TempStream();
    UserTodosDao = new UserTodosDao(_databaseStream);
  }

  public UserTodosDao UserTodosDao { get; }

  public void Dispose()
  {
    _databaseStream.Dispose();
  }
}