using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
  public static class IdentityServiceExtensions
  {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
      var builder = services.AddIdentityCore<AppUser>();

      builder = new IdentityBuilder(builder.UserType, builder.Services);
      //여기에 유저매니저가 있따
      builder.AddEntityFrameworkStores<AppIdentityDbContext>();
      builder.AddSignInManager<SignInManager<AppUser>>();
      //singin manager 를 애드하고 애드오센티케이션을 해줘야한다 
      //왜냐하면 사인인 매니저는 애드오센티케이션에 의지하고 있기 때문 
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            //설정할때 토큰 이슈어하고 오디언스 다 필요하다 아니면 에러남
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
            ValidIssuer = config["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false

          };
        });

      return services;

    }
  }
}