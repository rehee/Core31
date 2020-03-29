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
using Microsoft.Extensions.Hosting;

namespace Core31
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<MyDBContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));

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



      app.UseStaticFiles();

      app.UseHttpsRedirection();

      app.UseAuthentication();

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
