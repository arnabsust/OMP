using OMP.Service.Interface;
using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OMP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private IOrderService orderService;
        private IProductService productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartListVM"></param>
        /// <returns></returns>
        [Route("Create")]
        public HttpResponseMessage PostOrder(List<CartViewModel> cartListVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderViewModel orderVM = new OrderViewModel
                    {
                        CustomerName = User.Identity.Name
                    };

                    string orderNo = orderService.CreateNewOrder(cartListVM, orderVM);

                    foreach(var item in cartListVM)
                    {
                        productService.UpdatePopularity(item.ProductID);
                        productService.UpdateStockQuantity(item.ProductID, item.Quantity);
                    }
                    
                    return Request.CreateResponse(HttpStatusCode.Created, orderNo);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetAllOrder")]
        public List<OrderDetailsViewModel> GetAllOrder()
        {
            var orderList = orderService.GetAllOrder(User.Identity.Name);

            return orderList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetAllPurchaseHistory")]
        public List<OrderDetailsViewModel> GetAllPurchaseHistory()
        {
            var orderList = orderService.GetAllPurchaseHistory(User.Identity.Name);

            return orderList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buyerName"></param>
        /// <param name="sellerName"></param>
        /// <returns></returns>
        [Route("GetAllOrderByBuyer")]
        public List<OrderDetailsViewModel> GetAllOrderByBuyer(string buyerName, string sellerName)
        {
            var orderList = orderService.GetAllOrderByBuyer(buyerName, sellerName);

            return orderList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("UpdateOrderStatusAsDelivered")]
        public HttpResponseMessage PostUpdateOrderStatusAsDelivered(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    orderService.UpdateOrderStatus(id, 2);

                    return Request.CreateResponse(HttpStatusCode.Created, true);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("UpdateOrderStatusAsRejected")]
        public HttpResponseMessage PostUpdateOrderStatusAsRejected(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    orderService.UpdateOrderStatus(id, 0);

                    return Request.CreateResponse(HttpStatusCode.Created, true);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
