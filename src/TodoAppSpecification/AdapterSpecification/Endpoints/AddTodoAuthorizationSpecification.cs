using TodoAppSpecification.AdapterSpecification.Endpoints.Automation;

namespace TodoAppSpecification.AdapterSpecification.Endpoints;

/// <summary>
/// bug combine most/all of these tests into a data driven test
/// bug should these tests specify interaction with support?
/// </summary>
public class AddTodoAuthorizationSpecification
{
  [Test]
  public async Task ShouldNotAllowExpiredTokensInAddTodoRequest()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithHeader("Authorization", $"Bearer {TestTokens.GenerateExpiredToken()}")
    );

    //THEN
    httpResponseMock.ShouldBeForbidden401();
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadKeyInAddTodoRequest()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithHeader("Authorization", $"Bearer {TestTokens.GenerateTokenWithBadKey()}")
    );

    //THEN
    httpResponseMock.ShouldBeForbidden401();
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadIssuerInAddTodoRequest()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithHeader("Authorization", $"Bearer {TestTokens.GenerateTokenFromBadIssuer()}")
    );

    //THEN
    httpResponseMock.ShouldBeForbidden401();
  }

  [Test]
  public async Task ShouldNotAllowTokensWithNoBearerToken()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithoutHeader("Authorization")
    );

    //THEN
    httpResponseMock.ShouldBe400BadRequest();
  }

  [Test]
  public async Task ShouldNotAllowTokensWithBadlyFormattedBearerToken()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithHeader("Authorization", "Bearer lol")
    );

    //THEN
    httpResponseMock.ShouldBeForbidden401();
  }

  [Test]
  public async Task ShouldNotAllowTokensWithNoBearerInAuthorizationHeader()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithHeader("Authorization", "Bearer lol")
    );

    //THEN
    httpResponseMock.ShouldBeForbidden401();
  }

  [Test]
  public async Task ShouldNotAllowTokensWithNullAuthorizationHeader()
  {
    //GIVEN
    var driver = new EndpointsAdapterDriver();

    //WHEN
    var httpResponseMock = await driver.AttemptToAddTodoItem(
      request => request.WithHeader("Authorization", null)
    );

    //THEN
    httpResponseMock.ShouldBe400BadRequest();
  }
}