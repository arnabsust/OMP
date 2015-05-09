using OMP.Domain.Model;
using OMP.Domain.Repositories;
using OMP.Service.Interface;
using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Service
{
    public class OrderService : IOrderService
    {
        private UnitOfWork unitOfWork;
        private Order order;
        private OrderDetails orderDetails;

        public OrderService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method create new order.
        /// </summary>
        /// <param name="cartListVM"></param>
        /// <param name="orderVM"></param>
        /// <returns></returns>
        public string CreateNewOrder(List<CartViewModel> cartListVM, OrderViewModel orderVM)
        {
            order = new Order
            {
                OrderNo = GetNewOrderNo(),
                OrderDate = DateTime.Now,
                
                OrderStatus = 1,
                PaymentMethod = orderVM.PaymentMethod,
                ShippingCost = orderVM.ShippingCost,
                ShippingMethod = orderVM.ShippingMethod,

                CustomerName = orderVM.CustomerName
            };

            foreach(var product in cartListVM)
            {
                orderDetails = new OrderDetails
                {
                    ProductID = product.ProductID,
                    Quantity = product.Quantity,
                    UnitPrice = product.UnitPrice
                };

                order.OrderDetails.Add(orderDetails);
            }

            unitOfWork.OrderRepository.Insert(order);
            unitOfWork.Save();

            return order.OrderNo;
        }

        /// <summary>
        /// This method generate order no.
        /// </summary>
        /// <returns></returns>
        private string GetNewOrderNo()
        {
            string newReferenceNo = string.Empty;

            var result = (from c in unitOfWork.OrderRepository.Get()
                          orderby c.OrderID descending
                          select c.OrderNo).FirstOrDefault();

            if (result == null)
            {
                newReferenceNo = "REF-" + DateTime.Now.Year.ToString() + "-00001";
            }
            else
            {
                string newReferenceNoInDigit = (Convert.ToInt32(result.Split('-').Last()) + 1).ToString().PadLeft(5, '0');

                newReferenceNo = "REF-" + DateTime.Now.Year.ToString() + "-" + newReferenceNoInDigit;
            }

            return newReferenceNo;
        }
        

        /// <summary>
        /// This method return Order Status
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="orderStatus"></param>
        public void UpdateOrderStatus(int OrderID, int orderStatus)
        {
            unitOfWork.userProfileRepository.RawQuery("UPDATE [Order] SET OrderStatus = "+ orderStatus +" WHERE OrderID = " + OrderID);
        }

        /// <summary>
        /// This method return all order by a seller.
        /// </summary>
        /// <param name="sellerName"></param>
        /// <returns></returns>
        public List<OrderDetailsViewModel> GetAllOrder(string sellerName)
        {
            var orderList = (from o in unitOfWork.OrderRepository.Get()
                             join od in unitOfWork.OrderDetailsRepository.Get() on o.OrderID equals od.OrderID
                             join p in unitOfWork.CQRS_ProductSummaryRepository.Get() on od.ProductID equals p.ProductID
                             where p.UserName == sellerName
                             select new OrderDetailsViewModel
                             {
                                 OrderDetailsID = od.OrderDetailsID,
                                 OrderID = o.OrderID,
                                 OrderNo = o.OrderNo,
                                 OrderDate = o.OrderDate,
                                 OrderStatus = o.OrderStatus,

                                 ProductID = od.ProductID,
                                 ProductTitle = p.ProductTitle,
                                 ImageUrl = p.ImageUrl,
                                 Quantity = od.Quantity,
                                 UnitPrice = od.UnitPrice,

                                 BuyerName = p.UserName,
                                 SellerName = o.CustomerName
                             }).Distinct().ToList();

            return orderList;
        }

        /// <summary>
        /// This method return all order history by a buyer.
        /// </summary>
        /// <param name="buyerName"></param>
        /// <returns></returns>
        public List<OrderDetailsViewModel> GetAllPurchaseHistory(string buyerName)
        {
            var orderList = (from o in unitOfWork.OrderRepository.Get()
                             join od in unitOfWork.OrderDetailsRepository.Get() on o.OrderID equals od.OrderID
                             join p in unitOfWork.CQRS_ProductSummaryRepository.Get() on od.ProductID equals p.ProductID
                             where o.CustomerName == buyerName
                             select new OrderDetailsViewModel
                             {
                                 OrderDetailsID = od.OrderDetailsID,
                                 OrderID = o.OrderID,
                                 OrderNo = o.OrderNo,
                                 OrderDate = o.OrderDate,
                                 OrderStatus = o.OrderStatus,

                                 ProductID = od.ProductID,
                                 ProductTitle = p.ProductTitle,
                                 ImageUrl = p.ImageUrl,
                                 Quantity = od.Quantity,
                                 UnitPrice = od.UnitPrice,

                                 BuyerName = p.UserName,
                                 SellerName = o.CustomerName
                             }).Distinct().ToList();

            return orderList;
        }

        /// <summary>
        /// This method return all order placed by a buyer for certain seller.
        /// </summary>
        /// <param name="buyerName"></param>
        /// <param name="sellerName"></param>
        /// <returns></returns>
        public List<OrderDetailsViewModel> GetAllOrderByBuyer(string buyerName, string sellerName)
        {
            var orderList = (from o in unitOfWork.OrderRepository.Get()
                             join od in unitOfWork.OrderDetailsRepository.Get() on o.OrderID equals od.OrderID
                             join p in unitOfWork.CQRS_ProductSummaryRepository.Get() on od.ProductID equals p.ProductID
                             where o.CustomerName == buyerName && p.UserName == sellerName
                             select new OrderDetailsViewModel
                             {
                                 OrderDetailsID = od.OrderDetailsID,
                                 OrderID = o.OrderID,
                                 OrderNo = o.OrderNo,
                                 OrderDate = o.OrderDate,
                                 OrderStatus = o.OrderStatus,

                                 ProductID = od.ProductID,
                                 ProductTitle = p.ProductTitle,
                                 ImageUrl = p.ImageUrl,
                                 Quantity = od.Quantity,
                                 UnitPrice = od.UnitPrice,

                                 BuyerName = p.UserName,
                                 SellerName = o.CustomerName
                             }).Distinct().ToList();

            return orderList;
        }
    }
}
