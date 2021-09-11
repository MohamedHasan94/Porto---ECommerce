using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Data;
using Microsoft.AspNetCore.Identity;
using ITI.MVC.Core.Day2.Core;

namespace CEI_MVC_CORE_Proj.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork unit;
        public HomeController(IUnitOfWork _unit,ApplicationDbContext context)
        {
            unit = _unit;
        }
        public IActionResult Index()
        {
            
            return RedirectToAction("Index", "Products" );
        }

        public IActionResult About()
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
