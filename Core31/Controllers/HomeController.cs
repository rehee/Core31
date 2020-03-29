using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public IActionResult Edit(IFormFile name1)
    {
      var t = name1.GetType() == typeof(IFormFile);
      var t0 = name1.GetType() == typeof(FormFile);
      var t1 = name1.GetType();
      var t2 = typeof(List<IFormFile>);
      return Content("");
    }
  }
}