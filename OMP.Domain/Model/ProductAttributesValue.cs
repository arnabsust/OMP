using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class ProductAttributesValue
    {
        [Key]
        public int ProductAttributesValueID { get; set; }
        public int ProductID { get; set; }
        public string AttributesName { get; set; }
        public string AttributesValue { get; set; }
    }
}
