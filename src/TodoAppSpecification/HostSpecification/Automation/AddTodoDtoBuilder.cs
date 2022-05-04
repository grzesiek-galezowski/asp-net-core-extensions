using System.Collections.Generic;
using TodoApp.Http.AddTodo;

namespace TodoAppSpecification.HostSpecification.Automation;

public record AddTodoDtoBuilder
{
  public string Meeting { get; init; } = "Meeting " + Any.String();
  public string Content { get; init; } = "there's a meeting you need to attend " + Any.String();

  public AddTodoDto Build()
  {
    return new AddTodoDto(
      new AddTodoDataDto(Meeting, Content),
      new Dictionary<string, string>());
  }

  public static AddTodoDtoBuilder UniqueTodo()
  {
    return new AddTodoDtoBuilder();
  }
}