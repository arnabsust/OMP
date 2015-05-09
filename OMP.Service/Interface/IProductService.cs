using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IProductService
    {
        int CreateProduct(ProductViewModel productVM);
        void UpdateProduct(ProductViewModel productVM);
        void UpdateProductStatus(int productID, int status);
        ProductViewModel GetProductByID(int productID);
        Task<bool> CreateProductImage(ProductImageViewModel productImageVM);
        void DeleteProductImage(int productImageID);
        void UpdateRating(int productID, decimal rating);
        void UpdateStockQuantity(int productID, int orderQuantity);
        List<ProductImageViewModel> GetProductImageByProductID(int productID);
        void UpdateProductSummaryImage(int productID, string imageUrl);
        void UpdatePopularity(int productID);
    }
}
