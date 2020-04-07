using System.Linq;
using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace API
{
  public class Startup
  {
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddDbContext<StoreContext>(x =>
      {
        x.UseSqlite(_config.GetConnectionString("DefaultConnection"));
      });

      //싱글튼은 수명주기가 영원하다 앱이 돌아가는동안 
      services.AddSingleton<IConnectionMultiplexer>(c =>
      {
        var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
        return ConnectionMultiplexer.Connect(configuration);
      });

      services.AddApplicationServices();
      services.AddAutoMapper(typeof(MappingProfiles));
      services.AddSwaggerDocumnetation();
      services.AddCors(opt =>
      {
        opt.AddPolicy("policy", policy =>
        {
          policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
        });
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //미들웨어 파피프라인
      // if (env.IsDevelopment())
      // {
      //   app.UseDeveloperExceptionPage();
      // }
      app.UseMiddleware<ExceptionMiddelware>();

      //여기서 받은거를 Errorcontroller에 전달해서 실행시킬수 있게한다 
      app.UseStatusCodePagesWithReExecute("/errors/{0}");

      app.UseHttpsRedirection();

      app.UseRouting();
      //wwwroot use
      app.UseStaticFiles();

      app.UseAuthorization();
      app.UseCors("policy");
      app.useSwaggerDocumentation();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
