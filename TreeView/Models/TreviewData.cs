using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.Models
{
   
    public class TreeViewData
    {
        public string id { get; set; }

        public string text { get; set; }
        public string parentId { get; set; }
        public List<TreeViewData> children { get; set; }
        public bool opened { get; set; } = false;

    }
  
    public class TreeViewContainer
    {
        public string parentId { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public object children { get; set; }
        public bool opened { get; set; } = false;
        public object a_attr { get; set; }
        public object state { get; set; }
        public TreeViewContainer AddChildrens(List<TreeViewData> srcdata, int level)
        {
            // рекурсивная загрузка дерева.  Загружаются только 2 уровня дочерних узлов (level < 2) 
           // recursive loading tree. Loads onli 2 levels childs nodes (level < 2) 
            if (level == 2) children = true; // для первого не загружаемого уровня устанавливаем children = true для появления символа открытия (здесь ">").
                                             // Мы не знаем, есть ли у этого уровня дочерние узлы, но дать возможность проверить и загрузить нужно дать.
                                             // for first not loaded level set children = true for enable open symbol (">")
                                             // We don`t know is this level has childs nodes, but give the opportunity to check and download need
            if (level < 2)
            {               
                level++;
                children = (from d in srcdata
                            where d.parentId == id
                            select
                            (new TreeViewContainer()
                            {
                                text = d.text,
                                id = d.id,
                                parentId = null,
                                state = new { d.opened },
                                opened = d.opened,
                                a_attr = new { href = "/Test2", },
                            }).AddChildrens(srcdata, level));
            }
            return this;
        }
    }


  

}
