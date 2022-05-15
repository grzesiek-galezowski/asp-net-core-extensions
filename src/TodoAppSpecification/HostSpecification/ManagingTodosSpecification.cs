using TodoAppSpecification.HostSpecification.Automation;

namespace TodoAppSpecification.HostSpecification;

public class ManagingTodosSpecification
{
  [Test]
  public async Task ShouldBeAbleToCreateATodo()
  {
    //GIVEN
    await using var driver = await TodoAppDriver.CreateInstance();

    //WHEN
    var addTodoResponse = await driver.AttemptToAddNewTodo(AddTodoDtoBuilder.UniqueTodo());

    //THEN
    addTodoResponse.ShouldBeSuccessful();
  }

  [Test]
  public async Task ShouldBeAbleToLinkTodos()
  {
    //GIVEN
    await using var driver = await TodoAppDriver.CreateInstance();

    var addTodo1Response = await driver.AddNewTodo(AddTodoDtoBuilder.UniqueTodo());
    var addTodo2Response = await driver.AddNewTodo(AddTodoDtoBuilder.UniqueTodo());

    var id1 = await addTodo1Response.GetTodoId();
    var id2 = await addTodo2Response.GetTodoId();

    //WHEN
    var linkResponse = await driver.AttemptToLinkTodos(id1, id2);

    //THEN
    linkResponse.ShouldBeSuccessful();
    //bug This is not enough. Why do we link the TODOs?
  }

}