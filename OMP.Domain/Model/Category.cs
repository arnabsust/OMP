using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public int ParentCategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
