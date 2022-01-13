using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Postgresql.Mvc.Data;
using Postgresql.Mvc.Models;

namespace Postgresql.Mvc.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext context;

    public HomeController(ApplicationDbContext context)
    {
      this.context = context;
    }

    public IActionResult Index()
    {

      context.Users.Select(x => x.UserName).ToList().ForEach(Console.WriteLine);
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
