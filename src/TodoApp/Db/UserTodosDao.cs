using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NullableReferenceTypesExtensions;
using TodoApp.Logic;

namespace TodoApp.Db;

public class UserTodosDao : IUserTodosDao
{
  private readonly TodoContext _todoContext;

  public UserTodosDao()
  {
    _todoContext = new TodoContext(); //bug should live longer/shorter?
  }

  public async Task SaveAsync(TodoCreatedData todoData, CancellationToken cancellationToken)
  {
    var linkedDtos = await _todoContext.Todos.Where(todo=> todoData.Links.Contains(todo.Id.Value)).ToArrayAsync(cancellationToken);
    var persistentTodoDto = new PersistentTodoDto
    {
      Id = todoData.Id,
      Content = todoData.Content,
      LinkedNotes = linkedDtos,
      Title = todoData.Title
    };
    await _todoContext.Todos.AddAsync(persistentTodoDto, cancellationToken).AsTask();
    await _todoContext.SaveChangesAsync(cancellationToken);
  }

  public async Task<TodoCreatedData> LoadAsync(Guid id, CancellationToken cancellationToken)
  {
    var persistentTodoDto = await _todoContext.Todos.FindAsync(id, cancellationToken);
    return new TodoCreatedData(
      persistentTodoDto.Id.Value, 
      persistentTodoDto.Title.OrThrow(), 
      persistentTodoDto.Content.OrThrow(),
      persistentTodoDto.LinkedNotes.OrThrow().Select(n => n.Id.Value).ToImmutableHashSet());
  }
}