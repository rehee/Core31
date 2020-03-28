using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core31.C
{
  [Area("Admin")]
  public class DefaultController: Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
