using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Invokable;
using TodoApp.Db;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoAppSpecification.Integration.Storage;

public class StorageAdapterSpecification
{
  [Test]
  public async Task ShouldAllowReadingSavedNotes()
  {
    //GIVEN
    using var storage = new StorageAdapter();
    var id = Any.Guid();
    var savedNote = Any.Instance<CreateTodoRequestData>();

    await storage.UserTodosDao.Save(id, savedNote, Any.CancellationToken());

    //WHEN
    var loadedNote = await storage.UserTodosDao.Load(id, Any.CancellationToken());

    //THEN
    loadedNote.Content.Should().Be(savedNote.Content);
    loadedNote.Title.Should().Be(savedNote.Title);
    loadedNote.Id.Should().Be(id);
    loadedNote.Links.Should().Equal(ImmutableHashSet<Guid>.Empty);
  }
}