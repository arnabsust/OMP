using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        public int ParentCategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryHierarchicalViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<CategoryHierarchicalViewModel> ChildList { get; set; }
    }
}
