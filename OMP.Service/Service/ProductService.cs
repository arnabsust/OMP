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
    public class ProductService : IProductService
    {
        private UnitOfWork unitOfWork;
        private Product product;
        private ProductImage productImage;
        private ProductAttributesValue productAttributesValue;
        private CQRS_ProductSummary productSummary;

        public ProductService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        public int CreateProduct(ProductViewModel productVM)
        {
            product = new Product
            {
                ProductCode = productVM.ProductCode,
                ProductTitle = productVM.ProductTitle,
                ProductDescription = productVM.ProductDescription,
                ProductShortDescription = productVM.ProductShortDescription,

                ProductStatus = productVM.ProductStatus,
                UnitPrice = productVM.UnitPrice,
                StockQuantity = productVM.StockQuantity,

                CategoryID = productVM.CategoryID,
                UserName = productVM.UserName
            };

            unitOfWork.ProductRepository.Insert(product);

            if (productVM.ProductAttributesValueList != null)
            {
                foreach (var attribute in productVM.ProductAttributesValueList)
                {
                    productAttributesValue = new ProductAttributesValue
                    {
                        ProductID = product.ProductID,
                        AttributesName = attribute.AttributesName,
                        AttributesValue = attribute.AttributesValue
                    };

                    unitOfWork.ProductAttributesValueRepository.Insert(productAttributesValue);
                }
            }

            CreateProductSummary(productVM);

            unitOfWork.Save();

            return product.ProductID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        public void UpdateProduct(ProductViewModel productVM)
        {
            product = GetProductEntityByID(productVM.ProductID);

            product.ProductCode = productVM.ProductCode;
            product.ProductTitle = productVM.ProductTitle;
            product.ProductDescription = productVM.ProductDescription;
            product.ProductShortDescription = productVM.ProductShortDescription;

            product.ProductStatus = productVM.ProductStatus;
            product.UnitPrice = productVM.UnitPrice;
            product.StockQuantity = productVM.StockQuantity;

            product.CategoryID = productVM.CategoryID;
            product.UserName = productVM.UserName;

            unitOfWork.ProductRepository.Update(product);

            var oldProductAttributesValueList = (from s in unitOfWork.ProductAttributesValueRepository.Get()
                                                 where s.ProductID == productVM.ProductID
                                                 select s).ToList();

            if(oldProductAttributesValueList.Count != 0)
            {
                foreach(var item in oldProductAttributesValueList)
                {
                    unitOfWork.ProductAttributesValueRepository.Delete(
                            new ProductAttributesValue { ProductAttributesValueID = item.ProductAttributesValueID }
                            );
                }
            }

            foreach (var attribute in productVM.ProductAttributesValueList)
            {
                productAttributesValue = new ProductAttributesValue
                {
                    ProductID = product.ProductID,
                    AttributesName = attribute.AttributesName,
                    AttributesValue = attribute.AttributesValue
                };

                unitOfWork.ProductAttributesValueRepository.Insert(productAttributesValue);
            }

            UpdateProductSummary(productVM);

            unitOfWork.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="status"></param>
        public void UpdateProductStatus(int productID, int status)
        {
            product = GetProductEntityByID(productID);
            product.ProductStatus = status;

            productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                              where s.ProductID == productID
                              select s).SingleOrDefault();

            productSummary.ProductStatus = status;

            unitOfWork.ProductRepository.Update(product);
            unitOfWork.CQRS_ProductSummaryRepository.Update(productSummary);
            unitOfWork.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productImageVM"></param>
        /// <returns></returns>
        public async Task<bool> CreateProductImage(ProductImageViewModel productImageVM)
        {
            if (productImageVM != null)
            {
                    productImage = new ProductImage
                    {
                        ProductID = productImageVM.ProductID,
                        ImageUrl = productImageVM.ImageUrl
                    };

                    unitOfWork.ProductImageRepository.Insert(productImage);
            }
            
            await unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productImageID"></param>
        public void DeleteProductImage(int productImageID)
        {
            unitOfWork.ProductImageRepository.Delete(new ProductImage { ProductImageID = productImageID });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<ProductImageViewModel> GetProductImageByProductID(int productID)
        {
            var productImageList = (from s in unitOfWork.ProductImageRepository.Get()
                                    where s.ProductID == productID
                                    select new ProductImageViewModel
                                    {
                                        ProductImageID = s.ProductImageID,
                                        ProductID = s.ProductID,
                                        ImageUrl = s.ImageUrl
                                    }).ToList();

            return productImageList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private Product GetProductEntityByID(int productID)
        {
            product = (from s in unitOfWork.ProductRepository.Get()
                       where s.ProductID == productID
                       select s).SingleOrDefault();

            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public ProductViewModel GetProductByID(int productID)
        {
            var product = (from s in unitOfWork.ProductRepository.Get()
                           join c in unitOfWork.categoryRepository.Get() on s.CategoryID equals c.CategoryID
                           where s.ProductID == productID
                           select new ProductViewModel
                           {
                               ProductID = s.ProductID,
                               ProductCode = s.ProductCode,
                               ProductTitle = s.ProductTitle,
                               ProductDescription = s.ProductDescription,
                               ProductShortDescription = s.ProductShortDescription,

                               ProductStatus = s.ProductStatus,
                               UnitPrice = s.UnitPrice,
                               StockQuantity = s.StockQuantity,

                               CategoryID = s.CategoryID,
                               CategoryName = c.CategoryName,
                               UserName = s.UserName,
                           }).SingleOrDefault();

            if(product != null)
            {
                product.ProductAttributesValueList = (from s in unitOfWork.ProductAttributesValueRepository.Get()
                                                      where s.ProductID == productID
                                                      select new ProductAttributesValueViewModel
                                                      {
                                                          ProductAttributesValueID = s.ProductAttributesValueID,
                                                          ProductID = s.ProductID,
                                                          AttributesName = s.AttributesName,
                                                          AttributesValue = s.AttributesValue
                                                      }).ToList();
            }

            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        private void CreateProductSummary(ProductViewModel productVM)
        {
            productSummary = new CQRS_ProductSummary
            {
                CategoryName = productVM.CategoryName,
                LastModifiedDate = DateTime.Now,
                Popularity = 0,
                ProductCode = productVM.ProductCode,
                ProductTitle = productVM.ProductTitle,
                ProductDescription = productVM.ProductDescription,
                ProductShortDescription = productVM.ProductShortDescription,
                ProductStatus = productVM.ProductStatus,
                Rating = 0,
                UnitPrice = productVM.UnitPrice,
                StockQuantity = productVM.StockQuantity,
                UserName = productVM.UserName
            };

            unitOfWork.CQRS_ProductSummaryRepository.Insert(productSummary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        private void UpdateProductSummary(ProductViewModel productVM)
        {
            productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                                                  where s.ProductID == productVM.ProductID
                                                  select s).SingleOrDefault();

            productSummary.CategoryName = productVM.CategoryName;
            productSummary.LastModifiedDate = DateTime.Now;
            productSummary.ProductCode = productVM.ProductCode;
            productSummary.ProductTitle = productVM.ProductTitle;
            productSummary.ProductDescription = productVM.ProductDescription;
            productSummary.ProductShortDescription = productVM.ProductShortDescription;
            productSummary.UnitPrice = productVM.UnitPrice;
            productSummary.StockQuantity = productVM.StockQuantity;
            
            unitOfWork.CQRS_ProductSummaryRepository.Update(productSummary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="imageUrl"></param>
        public void UpdateProductSummaryImage(int productID, string imageUrl)
        {
            productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                              where s.ProductID == productID
                              select s).SingleOrDefault();

            if (productSummary != null)
            {
                productSummary.ImageUrl = imageUrl;
                unitOfWork.CQRS_ProductSummaryRepository.Update(productSummary);
                unitOfWork.Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="rating"></param>
        public void UpdateRating(int productID, decimal rating)
        {
            productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                              where s.ProductID == productID
                              select s).SingleOrDefault();

            if(productSummary != null)
            {
                productSummary.Rating = rating;

                unitOfWork.CQRS_ProductSummaryRepository.Update(productSummary);
                unitOfWork.Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        public void UpdatePopularity(int productID)
        {
            productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                              where s.ProductID == productID
                              select s).SingleOrDefault();

            if (productSummary != null)
            {
                productSummary.Popularity++;

                unitOfWork.CQRS_ProductSummaryRepository.Update(productSummary);
                unitOfWork.Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="orderQuantity"></param>
        public void UpdateStockQuantity(int productID, int orderQuantity)
        {
            product = GetProductEntityByID(productID);
            product.StockQuantity = product.StockQuantity - orderQuantity;

            unitOfWork.ProductRepository.Update(product);

            productSummary = (from s in unitOfWork.CQRS_ProductSummaryRepository.Get()
                              where s.ProductID == productID
                              select s).SingleOrDefault();

            productSummary.StockQuantity = productSummary.StockQuantity - orderQuantity;

            unitOfWork.CQRS_ProductSummaryRepository.Update(productSummary);

            unitOfWork.Save();

            if (product.StockQuantity == 0)
            {
                UpdateProductStatus(productID, 0);
            }
        }
    }
}
