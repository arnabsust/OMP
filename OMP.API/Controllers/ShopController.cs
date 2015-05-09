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
    [RoutePrefix("api/Shop")]
    public class ShopController : ApiController
    {
        private IShopService shopService;

        public ShopController(IShopService shopService) 
        {
            this.shopService = shopService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopVM"></param>
        /// <returns></returns>
        [Route("Create")]
        public HttpResponseMessage PostShop(ShopViewModel shopVM) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    shopService.CreateShop(shopVM);
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
        /// <param name="shopVM"></param>
        /// <returns></returns>
        [Route("Update")]
        public HttpResponseMessage PutShop(ShopViewModel shopVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    shopService.UpdateShop(shopVM);
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
        /// <returns></returns>
        [Route("GetShop")]
        public ShopViewModel GetShop()
        {
            return null;
        }
    }
}
