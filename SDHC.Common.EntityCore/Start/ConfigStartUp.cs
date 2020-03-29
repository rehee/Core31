using Microsoft.EntityFrameworkCore;
using SDHC.Common.EntityCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Start
{
  public static class ConfigStartUp
  {
    public static void Init<TRepo, TBaseContent, TBaseSelect>(
      Func<TRepo> repoCreate, string webBasePath
      ) 
      where TRepo : DbContext, IContent, new()
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
    {

    }
  }
}
