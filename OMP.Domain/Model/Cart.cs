using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
        public int ProductCriteriaID { get; set; }
        public int ProductCriteriaValueID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CartAddDate { get; set; }
    }
}
