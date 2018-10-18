using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TreeView.Models;
using Newtonsoft.Json;
namespace TreeView.Controllers
{
    /// <summary>
    /// Собственно котроллер дерева
    /// Собственного представления не имеет, не нужнО.
    /// </summary>
    public class testController : Controller
    {
        // Поставщик данных 
        private readonly IObjectRepositary repo;
        public testController(IObjectRepositary r)
        {     
            repo = r;
        }
        // Выдача дерева по запросу из клиента (jstree)
        // Generation tree by client (browser jstree) query 
        public JsonResult GetNodesJsTree(string id)
        {
            GetPaths();
            return Json(repo.GetDataTree(id, TreeViewPathProvider.Instance().openedNodes));
        }
        private void GetPaths()
        {
            // получаем строку с открытыми узлами дерева из сессии
            // getting string with opened nodes fron Session
            if (!HttpContext.Session.Keys.Contains("ONK"))
            {
                HttpContext.Session.SetString("ONK", "");
            }
            // сохраняем строку в TreeViewPathProvider, одновременно генерируетс массив строк с id открытых узлов
            // save string inti TreeViewPathProvider, and making string array with id`s of opened nodes
            TreeViewPathProvider.Instance().OpenNodesString = HttpContext.Session.GetString("ONK");
        }
        private void SavePaths()
        {
            // сохраняем строку с открытыми узлами дерева в сессии
            HttpContext.Session.SetString("ONK", TreeViewPathProvider.Instance().OpenNodesString);
        }
        public void CloseNode(string id)
        {
            // отрабатываем событие в дереве по сворачиванию узла
            // handle event of closing tree node
            TreeViewPathProvider.Instance().DelNode(id);
            SavePaths();
        }
        public void OpenNode(string id)
        {
            // отрабатываем событие в дереве по разворачиванию узла
            // handle event of opening tree node
            string parid = repo.Datas.Where(d => d.id == id).Single().parentId;
            TreeViewPathProvider.Instance().AddNode(parid,id);
            SavePaths();
        }
        

    }
}
