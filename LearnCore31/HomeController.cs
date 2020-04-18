using LearnCore31.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnCore31
{
  public class HomeController : Controller
  {

    public HomeController()
    {

    }
    public async Task<IActionResult> Index()
    {
      return Content("this is test");
    }
  }
}
