# HttpContextMock

```csharp
[Test]
public async Task ShouldRespondToPostTodoWithTodoCreated()
{
  //GIVEN
  var idGenerator = Substitute.For<IIdGenerator>();
  var id = Any.String();
  var context = HttpContextMock.Default();
  var httpRequest = context.Request().PostJson(new TodoDto
  {
    Title = "a",
    Content = "b"
  }).RealInstance;
  var httpResponse = context.Response().RealInstance;
  var action = new AddTodoAction(
    httpRequest, 
    httpResponse, 
    idGenerator);

  idGenerator.Generate().Returns(id);

  //WHEN
  await action.ExecuteAsync();

  //THEN
  context.Response().Should().HaveBody(new TodoCreatedDto
  {
    Title = "a",
    Content = "b",
    Id = id
  });
}
```
