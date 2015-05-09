using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductShortDescription { get; set; }
        public string ProductDescription { get; set; }

        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ProductStatus { get; set; }
        public int CategoryID { get; set; }
        public string UserName { get; set; }
    }
}
