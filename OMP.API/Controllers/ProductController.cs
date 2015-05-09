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
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private IProductService productService;
        private ICategoryService categoryService;
        private ICQRSService cqrsService;
        private IProductReviewService productReviewService;
        private ISearchService searchService;

        public ProductController(IProductService productService, ICategoryService categoryService,
                                    ICQRSService cqrsService, IProductReviewService productReviewService,
                                        ISearchService searchService) 
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.cqrsService = cqrsService;
            this.productReviewService = productReviewService;
            this.searchService = searchService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        [Route("Create")]
        public HttpResponseMessage PostProduct(ProductViewModel productVM) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = categoryService.GetCategoryByID(productVM.CategoryID);

                    productVM.UserName = User.Identity.Name;
                    productVM.CategoryName = category.CategoryName;
                    
                    int productID = productService.CreateProduct(productVM);

                    return Request.CreateResponse(HttpStatusCode.Created, productID);
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
        /// <param name="productImageID"></param>
        /// <returns></returns>
        [Route("PostDeleteProductImage")]
        public HttpResponseMessage PostDeleteProductImage(int productImageID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productService.DeleteProductImage(productImageID);

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
        /// <param name="productID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetProductByID")]
        public ProductViewModel GetProductByID(int productID)
        {
            var products = productService.GetProductByID(productID);

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetProductImageByProductID")]
        public List<ProductImageViewModel> GetProductImageByProductID(int productID)
        {
            var products = productService.GetProductImageByProductID(productID);

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetProductSummaryByID")]
        public CQRS_ProductSummary GetProductSummaryByID(int productID)
        {
            var products = cqrsService.GetProductSummaryByID(productID);

            if(User.Identity.Name != null)
            {
                searchService.StoreSearchProduct(productID, User.Identity.Name);
            }

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryName"></param>
        /// <param name="filterByUserName"></param>
        /// <param name="lowestPrice"></param>
        /// <param name="highestPrice"></param>
        /// <param name="sortByMostSold"></param>
        /// <param name="sortByPriceLowToHigh"></param>
        /// <param name="sortbyPriceHighToLow"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [Route("GetAllProducts")]
        public List<CQRS_ProductSummary> GetAllProducts(int pageNumber, int pageSize = 5,
                string categoryName = "", string filterByUserName = "", decimal lowestPrice = 0, decimal highestPrice = 0,
                bool sortByMostSold = false, bool sortByPriceLowToHigh = false, bool sortbyPriceHighToLow = false, string keyword = "")
        {
            var products = cqrsService.GetAllProducts(pageNumber, pageSize,
                                categoryName, filterByUserName, lowestPrice, highestPrice,
                                    sortByMostSold, sortByPriceLowToHigh, sortbyPriceHighToLow, keyword);

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryName"></param>
        /// <param name="filterByUserName"></param>
        /// <param name="lowestPrice"></param>
        /// <param name="highestPrice"></param>
        /// <param name="sortByMostSold"></param>
        /// <param name="sortByPriceLowToHigh"></param>
        /// <param name="sortbyPriceHighToLow"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Route("MyProducts")]
        public List<CQRS_ProductSummary> GetAllProductsSecured(int pageNumber, int pageSize = 5,
                string categoryName = "", string filterByUserName = "", decimal lowestPrice = 0, decimal highestPrice = 0,
                bool sortByMostSold = false, bool sortByPriceLowToHigh = false, bool sortbyPriceHighToLow = false, string keyword = "")
        {
            string userName = User.Identity.Name;

            var products = cqrsService.GetAllProducts(pageNumber, pageSize,
                                categoryName, userName, lowestPrice, highestPrice,
                                    sortByMostSold, sortByPriceLowToHigh, sortbyPriceHighToLow, keyword);

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        [Route("Update")]
        public HttpResponseMessage PutProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productService.UpdateProduct(productVM);
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
        /// <param name="productID"></param>
        /// <param name="reviewText"></param>
        /// <returns></returns>
        [Route("CreateProductReview")]
        public HttpResponseMessage PostProductReview(int productID, string reviewText)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductReviewViewModel productReviewVM = new ProductReviewViewModel();
                    productReviewVM.ProductID = productID;
                    productReviewVM.ReviewText = reviewText;
                    productReviewVM.ReviewedDate = DateTime.Now;
                    productReviewVM.CustomerName = User.Identity.Name;

                    productReviewService.CreateReview(productReviewVM);

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
        /// <param name="productID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetAllReviewByProductID")]
        public List<ProductReviewViewModel> GetAllReviewByProductID(int productID)
        {
            var productReviewList = productReviewService.GetAllReviewByProductID(productID);

            return productReviewList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetRelatedProducts")]
        public List<CQRS_ProductSummary> GetRelatedProduct(int productID, string categoryName)
        {
            var categoryList = categoryService.GetCategoryFromSearch(User.Identity.Name);
            var productList = cqrsService.GetRelatedProduct(productID, categoryName);

            return productList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetRecommendedProducts")]
        public List<CQRS_ProductSummary> GetRecommendedProduct()
        {
            var categoryListFromWishList = categoryService.GetCategoryFromWishList(User.Identity.Name);
            var categoryListFromSearch = categoryService.GetCategoryFromSearch(User.Identity.Name);

            var productList = cqrsService.GetRecommendedProduct(categoryListFromWishList, categoryListFromSearch);

            return productList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buyerName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetPurchasedProductByBuyer")]
        public List<CQRS_ProductSummary> GetPurchasedProductByBuyer(string buyerName)
        {
            var productList = cqrsService.GetPurchasedProductByBuyer(buyerName);

            return productList;
        }
    }
}
