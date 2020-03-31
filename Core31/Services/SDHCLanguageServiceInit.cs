using Microsoft.AspNetCore.Http;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Services
{
  public class SDHCLanguageServiceInit : ISDHCLanguageServiceInit
  {
    private IHttpContextAccessor acce { get; }

    private ISession session { get; }
    public SDHCLanguageServiceInit(IHttpContextAccessor acce)
    {
      this.acce = acce;
      this.session = acce != null && acce.HttpContext != null && acce.HttpContext.Session != null ? acce.HttpContext.Session : null;
    }
    public string LanguageKey => "Lang";
    public Func<string, object> getSession => (key) =>
    {
      if (session == null)
      {
        return null;
      }
      return session.GetInt32(key);
    };
    public Action<string, object> setSession => (key, value) =>
    {
      if (this.session == null)
        return;
      session.SetInt32(key, (int)value);
    };
  }
}
