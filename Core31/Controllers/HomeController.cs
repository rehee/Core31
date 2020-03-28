using Core31.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Controllers
{
  public class HomeController : Controller
  {
    private IClock clock { get; set; }
    public HomeController(IClock clock)
    {
      this.clock = clock;
    }

    public ActionResult Index()
    {
      Console.WriteLine("1");
      return Content("");
    }
  }
}
