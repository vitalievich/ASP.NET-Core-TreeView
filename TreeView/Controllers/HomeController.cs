using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TreeView.Models;

using Microsoft.AspNetCore.Http;

namespace TreeView.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObjectRepositary repo;

        public HomeController(IObjectRepositary r)
        {  
            repo = r;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

    }
}
