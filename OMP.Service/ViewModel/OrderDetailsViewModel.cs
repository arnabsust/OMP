using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int OrderDetailsID { get; set; }
        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public string BuyerName { get; set; }
        public string SellerName { get; set; }
    }
}
