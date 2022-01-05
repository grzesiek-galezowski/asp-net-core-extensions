using System;

namespace TodoApp
{
  public static class NullableReferenceTypesExtensions
  {
    public static T OrThrow<T>(this T? obj) => obj ?? throw new ArgumentNullException(nameof(obj));
  }
}
