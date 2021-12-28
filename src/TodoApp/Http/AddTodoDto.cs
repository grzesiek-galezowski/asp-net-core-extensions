using System.Collections.Generic;

namespace TodoApp.Http;

public record AddTodoDto(AddTodoDataDto Data, Dictionary<string, string> Links);