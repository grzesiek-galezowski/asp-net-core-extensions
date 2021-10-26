using System;
using System.Collections.Immutable;

namespace TodoApp.Logic;

public record TodoCreatedData(Guid Id, string Title, string Content, ImmutableHashSet<Guid> Links);

