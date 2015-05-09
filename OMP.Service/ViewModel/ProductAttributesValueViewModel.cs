using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class ProductAttributesValueViewModel
    {
        public int ProductAttributesValueID { get; set; }
        public int ProductID { get; set; }
        public string AttributesName { get; set; }
        public string AttributesValue { get; set; }
    }
}
