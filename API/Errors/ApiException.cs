namespace API.Errors
{
  public class ApiException : ApiResponse
  {

    public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
    {
      Dtails = details;
    }

    public string Dtails { get; set; }

  }
}