namespace TodoApp.Logic;

public interface ILinkTodoResponseInProgress
{
  void LinkedSuccessfully(TodoCreatedData todo1, TodoCreatedData todo2);
}