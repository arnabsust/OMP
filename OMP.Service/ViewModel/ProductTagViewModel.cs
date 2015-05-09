using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class ProductTagViewModel
    {
        public int ProductTagID { get; set; }
        public int ProductID { get; set; }
        public int TagID { get; set; }
        public string TagName { get; set; }
    }
}
