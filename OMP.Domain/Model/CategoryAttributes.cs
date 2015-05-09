using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class CategoryAttributes
    {
        [Key]
        public int CategoryAttributesID { get; set; }
        public int CategoryID { get; set; }
        public string AttributesName { get; set; }
    }
}
