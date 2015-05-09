using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class Shop
    {
        [Key]
        public int ShopID { get; set; }
        public string UserName { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
