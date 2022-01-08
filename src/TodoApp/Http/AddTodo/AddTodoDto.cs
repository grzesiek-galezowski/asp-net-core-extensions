using System.Collections.Generic;

namespace TodoApp.Http.AddTodo;

public record AddTodoDto(AddTodoDataDto Data, Dictionary<string, string> Links);