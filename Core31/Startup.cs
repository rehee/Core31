using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core31.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SDHC.Common.Configs;
using SDHC.Common.EntityCore.Services;
using SDHC.Common.Services;

namespace Core31
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      WebHostEnvironment = env;
    }
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment WebHostEnvironment { get; }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      var systemConfigKey = "SystemConfig";
      services.Configure<SystemConfig>(Configuration.GetSection(systemConfigKey));
      services.AddSession();
      services.AddHttpContextAccessor();
      Action<DbContextOptionsBuilder> dbAction = options =>
      {
        options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection"));
      };
      services.AddScoped<ISDHCLanguageServiceInit, SDHCLanguageServiceInit>();
      services.AddDbContext<MyDBContext>(dbAction);
      services.InitSDHC<MyDBContext, BaseContentModel, BaseSelectModel, FormFile>(Configuration, dbAction,
               WebHostEnvironment.ContentRootPath, systemConfigKey);

      services.AddControllersWithViews();
      services.ConfigureOptions(typeof(V.EditorRCLConfigureOptions));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      var p = env.ContentRootPath;


      app.UseStaticFiles();

      app.UseHttpsRedirection();

      app.UseAuthentication();
      app.UseSession();
      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
        name: "area",
        pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
