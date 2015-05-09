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
    public class ProductReviewService : IProductReviewService
    {
        private UnitOfWork unitOfWork;
        private ProductReview productReview;

        public ProductReviewService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method is used to create review.
        /// </summary>
        /// <param name="productReviewVM"></param>
        /// <returns></returns>
        public int CreateReview(ProductReviewViewModel productReviewVM)
        {
            productReview = new ProductReview
            {
                ProductID = productReviewVM.ProductID,
                CustomerName = productReviewVM.CustomerName,
                ReviewText = productReviewVM.ReviewText,
                ReviewedDate = productReviewVM.ReviewedDate
            };

            unitOfWork.ProductReviewRepository.Insert(productReview);
            unitOfWork.Save();

            return productReview.ProductReviewID;
        }

        /// <summary>
        /// This method returns all product review for certain product.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public List<ProductReviewViewModel> GetAllReviewByProductID(int ProductID) {
            var productReviewList = (from s in unitOfWork.ProductReviewRepository.Get()
                                     where s.ProductID == ProductID
                                     select new ProductReviewViewModel
                                {
                                    ProductReviewID = s.ProductReviewID,
                                    ProductID = s.ProductID,
                                    CustomerName = s.CustomerName,
                                    ReviewText = s.ReviewText,
                                    ReviewedDate = s.ReviewedDate
                                }).ToList();

            return productReviewList;
        }

        /// <summary>
        /// This method return Product review information
        /// </summary>
        /// <param name="productReviewID"></param>
        /// <returns></returns>
        private ProductReview GetProductReviewEntityByID(int productReviewID)
        {
            productReview = (from s in unitOfWork.ProductReviewRepository.Get()
                             where s.ProductReviewID == productReviewID
                    select s).SingleOrDefault();

            return productReview;
        }

        /// <summary>
        /// This method returns product review information by product id
        /// </summary>
        /// <param name="productReviewID"></param>
        /// <returns></returns>
        public ProductReviewViewModel GetProductReviewByID(int productReviewID)
        {
            var productReview = (from s in unitOfWork.ProductReviewRepository.Get()
                                 where s.ProductReviewID == productReviewID
                                 select new ProductReviewViewModel
                                    {
                                        ProductReviewID = s.ProductReviewID,
                                        ProductID = s.ProductID,
                                        CustomerName = s.CustomerName,
                                        ReviewText = s.ReviewText,
                                        ReviewedDate = s.ReviewedDate
                                    }).SingleOrDefault();
            return productReview;
        }
    }
}
