using Microsoft.AspNetCore.Http;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Models
{
  public class ContentPropertyCore : ContentProperty
  {
    public new IFormFile File { get; set; }
  }
}
