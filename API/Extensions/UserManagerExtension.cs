using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
  public static class UserManagerExtension
  {
    public static async Task<AppUser> FIndByEmailWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
    {
      var email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

      return await input.Users.Include(x => x.Adress).SingleOrDefaultAsync(x => x.Email == email);

    }

    public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
    {
      var email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

      return await input.Users.SingleOrDefaultAsync(x => x.Email == email);

    }



  }
}