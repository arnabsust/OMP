using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IProductReviewService
    {
        int CreateReview(ProductReviewViewModel productReviewVM);
        List<ProductReviewViewModel> GetAllReviewByProductID(int productID);
    }
}
