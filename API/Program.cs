using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
  public class Program
  {
    //async 가 오면 다음에 Task를 해줘야 한다 
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      //asp.net이 라이프타임을 관리안해주기 때문에 이거는 using을 써줘야한다 
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        //프로그램에는 에러핸들링하는게 없기 때문에 트라이캣치를 써준다 
        try
        {
          var context = services.GetRequiredService<StoreContext>();
          await context.Database.MigrateAsync();
          await StoreContextSeed.SeedAsync(context, loggerFactory);

          var userManager = services.GetRequiredService<UserManager<AppUser>>();
          var identityContext = services.GetRequiredService<AppIdentityDbContext>();

          await identityContext.Database.MigrateAsync();
          await AppIdentityDbContextSeed.SeedUserAsync(userManager);

        }
        catch (System.Exception ex)
        {
          var logger = loggerFactory.CreateLogger<Program>();
          logger.LogError(ex, "An error occured during migrations");

        }
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
