using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }

        public int PaymentMethod { get; set; }
        public decimal? ShippingCost { get; set; }
        public int? ShippingMethod { get; set; }

        public int OrderStatus { get; set; }
    }
}
