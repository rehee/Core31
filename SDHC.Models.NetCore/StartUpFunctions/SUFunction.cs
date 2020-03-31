using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDHC.Common.Configs;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.EntityCore.Services;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Services
{
  public static class SUContainer
  {
    public static void SUFunction<TRepo, TBaseContent, TBaseSelect>(this IServiceCollection services,IConfiguration configuration, IWebHostEnvironment env)
      where TRepo : DbContext, IContent
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
    {
      var systemConfigKey = "SystemConfig";
      services.Configure<SystemConfig>(configuration.GetSection(systemConfigKey));
      services.AddSession();
      services.AddHttpContextAccessor();
      Action<DbContextOptionsBuilder> dbAction = options =>
      {
        options.UseSqlServer(
              configuration.GetConnectionString("DefaultConnection"));
      };
      services.AddScoped<ISDHCLanguageServiceInit, SDHCLanguageServiceInit>();
      services.AddDbContext<TRepo>(dbAction);
      services.InitSDHC<TRepo, TBaseContent, TBaseSelect, FormFile>(configuration, dbAction,
               env.ContentRootPath, systemConfigKey);

      services.AddControllersWithViews();
      services.ConfigureOptions(typeof(V.EditorRCLConfigureOptions));
    }
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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
