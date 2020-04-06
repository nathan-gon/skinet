using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  //어떻게 여기로 code를 전달 할수 있는가? 미들웨어~~
  [Route("errors/{code}")]
  [ApiExplorerSettings(IgnoreApi = true)]
  public class ErrorController : BaseAPiController
  {

    public IActionResult Error(int code)
    {
      return new ObjectResult(new ApiResponse(code));
    }

  }
}