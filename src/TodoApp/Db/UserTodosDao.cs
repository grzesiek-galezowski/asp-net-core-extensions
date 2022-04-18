using System;
using System.Collections.Immutable;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.NullableReferenceTypesExtensions;
using LiteDB;
using TodoApp.Logic.TodoNotes;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoApp.Db;

public class UserTodosDao : IUserTodosDao
{
  private readonly Stream _stream;

  public UserTodosDao(Stream stream)
  {
    _stream = stream;
  }

  public async Task Save(Guid id, CreateTodoRequestData todoData, CancellationToken cancellationToken)
  {
    using var liteDb = new LiteDatabase(_stream);
    liteDb.GetCollection<PersistentTodoDto>().Insert(
      new PersistentTodoDto
      {
        Id = id,
        Content = todoData.Content,
        LinkedNotes = new Guid[] { },
        Title = todoData.Title
      });
  }

  public async Task<TodoCreatedData> Load(Guid id, CancellationToken cancellationToken)
  {
    try
    {
      using var liteDb = new LiteDatabase(_stream);
      var persistentTodoDto = liteDb.GetCollection<PersistentTodoDto>().FindById(id);
      return new TodoCreatedData(
        persistentTodoDto.Id.OrThrow(),
        persistentTodoDto.Title.OrThrow(),
        persistentTodoDto.Content.OrThrow(),
        persistentTodoDto.LinkedNotes.OrThrow().ToImmutableHashSet());
    }
    catch (Exception ex)
    {
      throw new NoteNotFoundException(id, ex);
    }
  }
}