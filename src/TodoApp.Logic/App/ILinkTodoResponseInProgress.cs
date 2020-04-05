namespace TodoApp.Logic.App
{
  public interface ILinkTodoResponseInProgress
  {
    void LinkedSuccessfully(TodoCreatedDto todo1, TodoCreatedDto todo2);
  }
}