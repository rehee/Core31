using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDHC.Models.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.Models.NetCore.Services
{
  public interface ISDHCUserManager<TUser> where TUser : IdentityUser
  {
    Type BaseUser { get; }
    ContentTableHtmlView GetContentTableHtmlView(IEnumerable<TUser> users);
  }

  public class SDHCUserManager<TUser> : ISDHCUserManager<TUser> where TUser : IdentityUser
  {
    public SDHCUserManager(UserManager<TUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<TUser> signInManager)
    {
      UserManager = userManager;
      RoleManager = roleManager;
      SignInManager = signInManager;
      BaseUser = typeof(TUser);
    }
    public Type BaseUser { get; }
    public UserManager<TUser> UserManager { get; }
    public RoleManager<IdentityRole> RoleManager { get; }
    public SignInManager<TUser> SignInManager { get; }
    public ContentTableHtmlView GetContentTableHtmlView(IEnumerable<TUser> users)
    {
      var children = users == null ? Enumerable.Empty<TUser>() : users;
      var allowChild = BaseUser.GetObjectCustomAttribute<AllowChildrenAttribute>();
      IEnumerable<string> additionalList = allowChild != null && allowChild.TableList != null ? allowChild.TableList : new string[] { };
      long id = 0;
      var rowItems = children.Select(b =>
      {
        id++;
        var values = additionalList.Select(a => b.GetPropertyByKey(a)).ToList();
        return new ContentTableRowItem(id, values, b.GetType().GetRealType(), 0, b.Id);
      }).ToList();
      var result = new ContentTableHtmlView();
      if (allowChild != null && allowChild.DisableDelete)
      {
        result.DisableDelete = true;
      }
      result.TableHeaders = additionalList.Select(b => BaseUser.GetPropertyLabelByKey(b)).ToList();
      result.Rows = rowItems;
      return result;
    }
  }
}
