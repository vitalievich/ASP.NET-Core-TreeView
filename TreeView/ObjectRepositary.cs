using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeView.Models;
using System.Text.RegularExpressions;

namespace TreeView
{
    public interface IObjectRepositary
    {
        string openedid { get; set; }
        string closedid {get;set; }
        List<TreeViewData> Datas { get; set; }
        List<TreeViewContainer> GetDataTree(string parentId, string[] openedNodes);
    }

    // Поствщик данных
    public class ObjectRepositary : IObjectRepositary
    {
        public List<TreeViewData> Datas { get; set; }
        public  string openedid { get; set; }
        public string closedid { get; set; }
        public ObjectRepositary()
        {
            Datas = Getdatas();
        }
        // Формирование плоского списка узлов. Только для примера.
        private List<TreeViewData> Getdatas()
        {
            List<TreeViewData> Datas = new List<TreeViewData>();
            for (int i = 1; i<3;i++)
            {
                Datas.Add(new TreeViewData()
                {
                    id = $"{i}",
                    text = $"Grand Parent {i}",
                    parentId = "#"
                });
                for (int j=1;j<3;j++)
                {
                    Datas.Add(new TreeViewData()
                    {
                        id = $"{i}.{j}",
                        text = $"Parent {i}.{j}",
                        parentId= $"{i}"
                    });
                    for (int k=1;k<3;k++)
                    {
                        Datas.Add(new TreeViewData()
                        {
                            id = $"{i}.{j}.{k}",
                            text = $"Child {i}.{j}.{k}",
                            parentId = $"{i}.{j}"
                        });
                        for (int l = 1; l < 3; l++)
                        {
                            Datas.Add(new TreeViewData()
                            {
                                id = $"{i}.{j}.{k}.{l}",
                                text = $"Child {i}.{j}.{k}.{l}",
                                parentId = $"{i}.{j}.{k}"
                            });
                            for (int m = 1; m < 3; m++)
                            {
                                Datas.Add(new TreeViewData()
                                {
                                    id = $"{i}.{j}.{k}.{l}.{m}",
                                    text = $"Child {i}.{j}.{k}.{l}.{m}",
                                    parentId = $"{i}.{j}.{k}.{l}"
                                });
                                for (int n = 1; n < 3; n++)
                                {
                                    Datas.Add(new TreeViewData()
                                    {
                                        id = $"{i}.{j}.{k}.{l}.{m}.{n}",
                                        text = $"Child {i}.{j}.{k}.{l}.{m}.{n}",
                                        parentId = $"{i}.{j}.{k}.{l}.{m}"
                                    });
                                }
                            }
                        }
                    }
                }
            }
            return Datas;
        }
        // установка признака открытости для узлов списка.
        private void GetOpenedNodesArray(string[] openedNodes)
        {
            (from d in Datas join o in openedNodes on d.id equals o select d).ToList().ForEach(x => x.opened = true);
        }

        // Формирование дерева для jstree
        public List<TreeViewContainer> GetDataTree(string id, string[] openedNodes)
        {
            GetOpenedNodesArray(openedNodes);
            List<TreeViewContainer> result = new List<TreeViewContainer>();
            result = (from d in Datas where d.parentId == id
                      select (new TreeViewContainer()
                                {
                                text = d.text,
                                id = d.id,
                                parentId = null,
                                state = new { d.opened },
                                opened =  d.opened,
                                a_attr = new { href = "/Test2", }
                                }).AddChildrens(Datas, 0)).ToList();
            return result;
        }
    }

    /// <summary>
    /// Для манипулирования со строкой данных открытых узлов.
    /// Формат строки: <id1><id2></id2></id1><id3></id3> 
    /// </summary>
    public class TreeViewPathProvider
    {
        private string openNodesString = string.Empty;
        private Regex rg = new Regex(@"<{1}(?<node>[^/>]+)>{1}", RegexOptions.Compiled);
        public string[] openedNodes { get; set; }

        private static TreeViewPathProvider tvpp;
        private TreeViewPathProvider()
        { }
        public static TreeViewPathProvider Instance()
        {
            if (tvpp == null) tvpp = new TreeViewPathProvider();
            return tvpp;
        }
       

        public string OpenNodesString
        {
            get { return openNodesString; }
            set
            {
                openNodesString = value;
                // формируем массив id открытых узлов
                openedNodes = (from m in rg.Matches(OpenNodesString) select m.Groups["node"].Value).ToArray(); 
            }
        }
        public TreeViewPathProvider AddNode(string parentId, string id)
        {
            // # -- виртуальный корень леса. 
            if (parentId == "#") // при разворачивании корня дерева (у него parentId == "#") забываем всю историю и начинаем новую
            {
                OpenNodesString = $"<{id}></{id}>";
            }
            else
            {
                // вставляем id открываемого узла.
                int start = OpenNodesString.IndexOf($"<{parentId}>") + $"<{parentId}>".Length;
                OpenNodesString = OpenNodesString.Insert(start, $"<{id}></{id}>");
            }
            return this;
        }
        public TreeViewPathProvider DelNode(string id)
        {
            // При закрытии узла удаляем его вместе со всем содержимым из OpenNodesString
            Regex rgr = new Regex($@"<{id}>.*</{id}>");
            OpenNodesString = rgr.Replace(OpenNodesString, "");
            return this;
        }     
    }
}
