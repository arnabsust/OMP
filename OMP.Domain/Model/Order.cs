using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }

        public int PaymentMethod { get; set; }
        public decimal? ShippingCost { get; set; }
        public int? ShippingMethod { get; set; }

        public int OrderStatus { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
