using OMP.Domain.Model;
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
    [RoutePrefix("api/WishList")]
    public class WishListController : ApiController
    {
        private IWishListService wishListService;
        public WishListController(IWishListService wishListService)
        {
            this.wishListService = wishListService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("Create")]
        public HttpResponseMessage PostWishList(int productID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int wishListID = wishListService.CreateWishList(productID, User.Identity.Name);

                    return Request.CreateResponse(HttpStatusCode.Created, wishListID);
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
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("Delete")]
        public HttpResponseMessage PostDeleteWishList(int productID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    wishListService.DeleteWithList(productID, User.Identity.Name);
                    return Request.CreateResponse(HttpStatusCode.OK, true);
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
        [Route("GetAllWishlist")]
        public List<CQRS_ProductSummary> GetAllWishList()
        {
            var wishList = wishListService.GetWishListByUser(User.Identity.Name);

            return wishList;
        }
    }
}
