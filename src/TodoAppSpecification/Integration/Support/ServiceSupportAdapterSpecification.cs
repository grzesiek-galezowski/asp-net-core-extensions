namespace TodoAppSpecification.Integration.Support;

public class ServiceSupportAdapterSpecification
{
  [Test]
  public void ShouldLogAuthorizationErrorAccordingToFormat()
  {
    //GIVEN
    using var driver = new ServiceSupportDriver();
    var customerId = Any.String();
    var requestId = Any.String();
    var operationName = Any.String();
    var exception = new Exception(Any.String());

    //WHEN
    using (driver.BeginScope(customerId, requestId, operationName))
    {
      driver.NotifyAuthorizationFailed(exception);
    }

    //THEN
    driver.LogCountShouldBe(1);
    driver.FirstLogShouldMatch(CurrentDateString() +
                                             $" *|0|ERROR|{typeof(ServiceSupportDriver).FullName}|" +
                                             $"Authorization failed System.Exception: {exception.Message}" +
                                             $"|requestId={requestId}" +
                                             $"|operationName={operationName}" +
                                             $"|customerId={customerId}");
  }

  public static string CurrentDateString()
  {
    return $"{DateTime.Now.Year.ToString("D2")}-{DateTime.Now.Month.ToString("D2")}-{DateTime.Now.Day.ToString("D2")}";
  }
}