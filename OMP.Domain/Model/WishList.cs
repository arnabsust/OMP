using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class WishList
    {
        [Key]
        public int WishListID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
    }
}
