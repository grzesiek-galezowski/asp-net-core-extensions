namespace TodoApp.Http.HttpValidation;

public class HttpHeaderMissingException : HttpRequestInvalidException
{
  public HttpHeaderMissingException(string headerName)
  : base($"Expected header {headerName} to be an existing, non-null value")
  {
  }
}