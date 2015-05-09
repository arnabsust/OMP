using OMP.Domain.Model;
using OMP.Domain.Repositories;
using OMP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Service
{
    public class CQRSService : ICQRSService
    {
        private UnitOfWork unitOfWork;

        public CQRSService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method returns Product summery for given product ID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public CQRS_ProductSummary GetProductSummaryByID(int productID)
        {
            var productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                                  where s.ProductID == productID
                                  select s).SingleOrDefault();

            return productSummary;
        }
        
        /// <summary>
        /// This method returns all prodcut information for given criteria. This method is used for product search
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryName"></param>
        /// <param name="userName"></param>
        /// <param name="lowestPrice"></param>
        /// <param name="highestPrice"></param>
        /// <param name="sortByMostSold"></param>
        /// <param name="sortByPriceLowToHigh"></param>
        /// <param name="sortbyPriceHighToLow"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<CQRS_ProductSummary> GetAllProducts(int pageNumber, int pageSize, 
                string categoryName, string userName, decimal lowestPrice, decimal highestPrice, 
                bool sortByMostSold, bool sortByPriceLowToHigh, bool sortbyPriceHighToLow, string keyword)
        {
            var productList = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                               orderby s.ProductID descending
                                select s
                                    )
                                    .Take(pageSize * pageNumber)
                                        .AsQueryable();

            if(categoryName != null && categoryName != string.Empty)
            {
                productList = productList
                                .Where(s => s.CategoryName.Contains(categoryName));
            }
            
            if(userName != null && userName != string.Empty)
            {
                productList = productList
                                .Where(s => s.UserName.Contains(userName));
            }

            if(lowestPrice != 0 || highestPrice != 0)
            {
                productList = productList.Where(x => x.UnitPrice >= lowestPrice && x.UnitPrice <= highestPrice);
            }

            if(keyword != null && keyword != string.Empty)
            {
                productList = productList.Where(x => x.ProductTitle.Contains(keyword) 
                        || x.ProductDescription.Contains(keyword) 
                        || x.ProductShortDescription.Contains(keyword)
                        || x.CategoryName.Contains(keyword));
            }

            if (sortByMostSold)
            {
                productList = productList.OrderByDescending(x => x.Popularity);
            }

            if (sortByPriceLowToHigh)
            {
                productList = productList.OrderBy(x => x.UnitPrice);
            }

            if (sortbyPriceHighToLow)
            {
                productList = productList.OrderByDescending(x => x.UnitPrice);
            }

            return productList.ToList();
        }

        /// <summary>
        /// This method returns recommended product for a certain user based on wishlist and search category history.
        /// </summary>
        /// <param name="categoryFromWishList"></param>
        /// <param name="categoryFromSearch"></param>
        /// <returns></returns>
        public List<CQRS_ProductSummary> GetRecommendedProduct(List<string> categoryFromWishList, List<string> categoryFromSearch)
        {
            categoryFromWishList.AddRange(categoryFromSearch);

            var productList = unitOfWork.CQRS_ProductSummaryRepository.Get()
                                .Where(x => categoryFromWishList.Contains(x.CategoryName))
                                    .OrderByDescending(x => x.LastModifiedDate)
                                        .ThenByDescending(x => x.Popularity)
                                            .Take(10)
                                                .ToList();

            return productList;
        }

        /// <summary>
        /// This method returns related product list for given product and category information
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public List<CQRS_ProductSummary> GetRelatedProduct(int productID, string categoryName)
        {
            var productList = unitOfWork.CQRS_ProductSummaryRepository.Get()
                                .Where(x => x.CategoryName == categoryName && x.ProductID != productID)
                                    .OrderByDescending(x=>x.LastModifiedDate)
                                        .ThenByDescending(x => x.Popularity)
                                            .Take(10)
                                                .ToList();

            return productList;
        }

        /// <summary>
        /// This method returns all product purchased by a user
        /// </summary>
        /// <param name="buyerName"></param>
        /// <returns></returns>
        public List<CQRS_ProductSummary> GetPurchasedProductByBuyer(string buyerName)
        {
            var purchasedProductList = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                                        join od in unitOfWork.OrderDetailsRepository.Get() on s.ProductID equals od.ProductID
                                        join o in unitOfWork.OrderRepository.Get() on od.OrderID equals o.OrderID
                                        where o.CustomerName == buyerName
                                        select s).Distinct().ToList();

            return purchasedProductList;
        }
    }
}
