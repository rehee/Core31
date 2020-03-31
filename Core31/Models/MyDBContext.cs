using Microsoft.EntityFrameworkCore;
using SDHC.Common.EntityCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Models
{
  public class MyDBContext : DbContext, IContent
  {
    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public DbSet<BaseContent> Contents { get; set; }
    public DbSet<BaseSelect> Selects { get; set; }
    public DbSet<BaseContentModel> BaseContentModels { get; set; }
    public DbSet<BaseSelectModel> BaseSelectModel { get; set; }



    public Task<int> SaveChangesAsync()
    {
      return this.SaveChangesAsync(default(System.Threading.CancellationToken));
    }
  }
}
