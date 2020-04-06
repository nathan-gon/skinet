using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
  public class ExceptionMiddelware
  {
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddelware> _logger;
    private readonly RequestDelegate _next;
    public ExceptionMiddelware(RequestDelegate next,
     ILogger<ExceptionMiddelware> logger, IHostEnvironment env)
    {
      _next = next;
      _logger = logger;
      _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      //문제가 없으면 넥스트 즉 context를 실행하라는 얘기
      try
      {
        await _next(context);
      }
      catch (System.Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _env.IsDevelopment()
                ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ApiResponse((int)HttpStatusCode.InternalServerError);

        var options = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };


        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);

      }
    }


  }
}