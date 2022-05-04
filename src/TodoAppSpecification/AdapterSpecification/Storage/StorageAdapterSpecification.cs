using System.Collections.Immutable;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Invokable;
using TodoApp.Db;
using TodoApp.Logic.TodoNotes.AddTodo;

namespace TodoAppSpecification.AdapterSpecification.Storage;

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
    var (guid, title, content, immutableHashSet) = await storage.UserTodosDao.Load(id, Any.CancellationToken());

    //THEN
    content.Should().Be(savedNote.Content);
    title.Should().Be(savedNote.Title);
    guid.Should().Be(id);
    immutableHashSet.Should().Equal(ImmutableHashSet<Guid>.Empty);
  }

  [Test]
  public async Task ShouldThrowExceptionWhenTryingToLoadNoteThatDoesNotExist()
  {
    //GIVEN
    var id = Any.Guid();
    using var storage = new StorageAdapter();

    //WHEN - THEN
    (await storage.Awaiting(s => s.UserTodosDao.Load(id, Any.CancellationToken()))
      .Should().ThrowExactlyAsync<NoteNotFoundException>()).WithMessage($"*{id}");
  }
}
