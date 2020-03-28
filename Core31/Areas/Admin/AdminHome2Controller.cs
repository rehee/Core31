using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core31.Areas.Admin
{
 [Area("Admin")]
  public class AdminHome2Controller : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}