using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core31.Controllers
{
  public class HomeController : Controller
  {
    private MyDBContext db { get; }
    public HomeController(MyDBContext db)
    {
      this.db = db;
    }
    public IActionResult Index()
    {
      var c = db.BaseContentModels.FirstOrDefault();
      return View();
    }
  }
}