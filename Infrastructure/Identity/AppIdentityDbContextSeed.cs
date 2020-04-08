using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
  public class AppIdentityDbContextSeed
  {
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
      if (!userManager.Users.Any())
      {
        var user = new AppUser
        {
          DisplayName = "Bob",
          Email = "bob@test.com",
          UserName = "bob@test.com",
          Adress = new Adress
          {
            FirstName = "bob",
            LastName = "Bobbity",
            Street = "10 th street",
            City = "ilsan",
            State = "goyang",
            Zipcode = "20014"
          }
        };
        await userManager.CreateAsync(user, "Pa$$w0rd");
      }
    }
  }
}