using System;

namespace API.Errors
{
  public class ApiResponse
  {
    public ApiResponse(int statusCode, string message = null)
    {
      StatusCode = statusCode;
      //?? 이표현은 널값이면 그다음것을 실행하라는 것이다 
      Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
      return statusCode switch
      {
        400 => "A Bad request , you have made",
        401 => "authorized you are not",
        404 => "resource found , it was not",
        500 => "Errors are the pathe to the dark side,",
        _ => null
      };
    }

  }
}