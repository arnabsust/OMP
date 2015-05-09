using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class ProductTag
    {
        [Key]
        public int ProductTagID { get; set; }
        public int ProductID { get; set; }
        public int TagID { get; set; }
    }
}
