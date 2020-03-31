using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ConfigStartUp
  {
    public static void InitSDHC<TRepo, TBaseContent, TBaseSelect, TFileSngle>([NotNullAttribute] this IServiceCollection serviceCollection,
      IConfiguration configuration, Action<DbContextOptionsBuilder> optionsAction, string basicRoot)
      where TRepo : DbContext, IContent
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
      where TFileSngle : IFormFile
    {
      IConfigurationSection sec = configuration.GetSection("SystemConfig");
      var type = typeof(SystemConfig);
      var obj = new SystemConfig();
      type.GetProperties().ToList().ForEach(p =>
      {
        var value = sec[p.Name];
        if (value != null)
        {
          try
          {
            p.SetValue(obj, value);
          }
          catch { }
        }
      });
      ConfigContainer.Systems = obj;
      var builder = new DbContextOptionsBuilder<TRepo>();
      optionsAction(builder);
      var options = builder.Options;
      var crudInit = new CrudSelectInit(
        () => Activator.CreateInstance(typeof(TRepo), options) as TRepo,
        typeof(TBaseContent), typeof(TBaseSelect)
      );
      CrudContainer.Crud = new BaseCruds(crudInit);
      CrudContainer.CrudModel = new CrudModel(crudInit);
      CrudContainer.CrudContent = new CrudContent(crudInit);
      ServiceContainer.ModelService = new ModelService(crudInit);
      ServiceContainer.ContentService = new ContentService(crudInit);
      ServiceContainer.SelectService = new SelectService(crudInit);
      //HostingEnvironment.MapPath("/"));

      ServiceContainer.SDHCFileService = new SDHCFileService(new SDHCFileConfig(
        basicRoot, ConfigContainer.Systems.FileUploadPath, new Dictionary<Type, SDHCSaveAble>()
        {
          [typeof(TFileSngle)] = new SDHCSaveAble(
            (input) =>
            {
              if (input == null)
                return null;
              return (input as IFormFile).FileName;
            }, (input, fileName) =>
            {
              if (input == null)
                return;
              using (var stream = System.IO.File.Create(fileName))
              {
                (input as IFormFile).CopyTo(stream);
              }
            }),
          [typeof(List<IFormFile>)] = new SDHCSaveAble(
            (input) =>
            {
              if (input == null)
                return null;
              return (input as IEnumerable<IFormFile>).FirstOrDefault().FileName;
            }, (input, fileName) =>
            {
              if (input == null)
                return;
              using (var stream = System.IO.File.Create(fileName))
              {
                (input as IEnumerable<IFormFile>).FirstOrDefault().CopyTo(stream);
              }
            }),
        }));

      //SelectManager.BasicSelectType = typeof(TBaseSelect);


      //ContentPostViewModel.GetContentPageUrl = () => G.ContentPageUrl;
      //ContentPostViewModel.GetContentViewPath = () => G.ContentViewPath;

      ContentPostViewModel.Convert = (input) => input.ConvertModelToPost();




    }
  }
}
