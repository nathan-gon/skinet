

using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

  public class BuggyController : BaseAPiController
  {
    private readonly StoreContext _context;
    public BuggyController(StoreContext context)
    {
      _context = context;
    }

    [HttpGet("notfound")]
    public async Task<ActionResult> GetNotFoundRequest()
    {
      var thing = await _context.Products.FindAsync(42);
      if (thing == null)
      {
        return NotFound(new ApiResponse(404)); // return in postman 404
      }

      return Ok();
    }


    [HttpGet("servererror")]
    public ActionResult GetServerErrorRequest()
    {
      var thing = _context.Products.FindAsync(889);

      var thingToretunr = thing.ToString();

      return Ok(thingToretunr);
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {

      return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {

      return Ok();
    }




  }
}