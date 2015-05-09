using OMP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface ICQRSService
    {
        CQRS_ProductSummary GetProductSummaryByID(int productID);

        List<CQRS_ProductSummary> GetAllProducts(int pageNumber, int pageSize,
                string categoryName, string userName, decimal lowestPrice, decimal highestPrice,
                bool sortByMostSold, bool sortByPriceLowToHigh, bool sortbyPriceHighToLow, string keyword);

        List<CQRS_ProductSummary> GetRecommendedProduct(List<string> categoryFromWishList, List<string> categoryFromSearch);

        List<CQRS_ProductSummary> GetRelatedProduct(int productID, string categoryName);

        List<CQRS_ProductSummary> GetPurchasedProductByBuyer(string buyerName);
    }
}
