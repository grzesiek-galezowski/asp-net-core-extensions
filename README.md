# HttpContextMock

```csharp
[Test]
public async Task ShouldRespondToPostTodoWithTodoCreated()
{
  //GIVEN
  var idGenerator = Substitute.For<IIdGenerator>();
  var action = new AddTodoAction(idGenerator);
  var id = Any.String();
  var context = HttpContextMock.Default();
  var httpRequest = context.Request().PostJson(new TodoDto
  {
    Title = "a",
    Content = "b"
  }).RealInstance;
  var httpResponse = context.Response().RealInstance;

  idGenerator.Generate().Returns(id);

  await action.ExecuteAsync(
      httpRequest, 
      httpResponse);

  //THEN
  context.Response().Should().HaveBody(new TodoCreatedDto
  {
    Title = "a",
    Content = "b",
    Id = id
  });
}
```
