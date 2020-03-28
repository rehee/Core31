using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core31.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Core31
{
  public class Startup
  {
    //private IServiceCollection _services { get; set; }
    //private IWebHostEnvironment _env { get; set; }
    //public Startup(IServiceCollection services, IWebHostEnvironment env)
    //{
    //  _services = services;
    //  _env = env;
    //}
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      //services.AddRazorPages();
      //services.AddMvc();

      services.AddSingleton<IClock, ChinaClock>();
      services.ConfigureOptions(typeof(V.EditorRCLConfigureOptions));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //http https?
      var assemblyView = Assembly.Load("V");
      var personEmbeddedFileProvider = new ManifestEmbeddedFileProvider(
          assemblyView
        );

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

        //endpoints.MapGet("/", async context =>
        //      {
        //    await context.Response.WriteAsync("Hello World!");
        //  });
      });
    }
  }
}
