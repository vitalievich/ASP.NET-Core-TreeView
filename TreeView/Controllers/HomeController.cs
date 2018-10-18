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
        /*
        public JsonResult GetNodes(int? parentId)
        {
            var res = (from r in repo.datas()
                       where r.ParentId == parentId
                       select new
                       {
                           r.id,
                           r.text,
                           r.hasChildren
                       });
            var re = Json(res);
            return Json(res);
        }
        
        public JsonResult GetNodesJsTree(string id)
        {
            var res = (from r in repo.datas()
                       where r.ParentId == id
                       select new
                       {
                           r.id,
                           text = "\u003ca href:\"Home\u002fContact\"\u003e" + r.text + "\u003ca\u003e", // r.text,
                           r.hasChildren
                       });
            var re = Json(res);
            return Json(res);
        }
        */
        //public JsonResult GetNodesJsTree(string id)
        //{
        //    if (!HttpContext.Session.Keys.Contains("ONK")) HttpContext.Session.SetString("ONK", "");
        //    var res = repo.GetDataTree(id, HttpContext.Session.GetString("ONK"));
        //    var re = Json(res);
        //    return Json(res);
        //}
        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */

    }
}