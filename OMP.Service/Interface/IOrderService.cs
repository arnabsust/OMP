using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IOrderService
    {
        string CreateNewOrder(List<CartViewModel> cartListVM, OrderViewModel orderVM);
        void UpdateOrderStatus(int OrderID, int orderStatus);
        List<OrderDetailsViewModel> GetAllOrder(string sellerName);
        List<OrderDetailsViewModel> GetAllPurchaseHistory(string buyerName);
        List<OrderDetailsViewModel> GetAllOrderByBuyer(string buyerName, string sellerName);
    }
}
