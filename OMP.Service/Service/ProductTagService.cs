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
    public class ProductTagService : IProductTagService
    {
        private UnitOfWork unitOfWork;
        private ProductTag productTag;

        public ProductTagService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productTagListVM"></param>
        /// <returns></returns>
        public bool CreateProductTag(List<ProductTagViewModel> productTagListVM)
        {
            foreach (var productTagVM in productTagListVM)
            {
                productTag = new ProductTag
                {
                    ProductTagID = productTagVM.ProductTagID,
                    ProductID = productTagVM.ProductID,
                    TagID = productTagVM.TagID
                };
                unitOfWork.ProductTagRepository.Insert(productTag);
            }
            unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<ProductTagViewModel> GetAllTagByProductID(int productID) {
            var productTagList = (from s in unitOfWork.ProductTagRepository.Get()
                                     join t in unitOfWork.TagRepository.Get() on s.ProductTagID equals t.TagID
                                     where s.ProductID == productID
                                      select new ProductTagViewModel
                                         {
                                             ProductTagID = s.ProductTagID,
                                             ProductID = s.ProductID,
                                             TagID = s.TagID,
                                             TagName = t.TagName
                                         }).ToList();

            return productTagList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        private ProductTag GetProductTagEntityByID(int tagID)
        {
            productTag = (from s in unitOfWork.ProductTagRepository.Get()
                          where s.TagID == tagID
                    select s).SingleOrDefault();

            return productTag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public ProductTagViewModel GetProductTagByID(int tagID)
        {
            var productTag = (from s in unitOfWork.ProductTagRepository.Get()
                        join t in unitOfWork.TagRepository.Get() on s.ProductTagID equals t.TagID
                        where s.TagID == tagID
                        select new ProductTagViewModel
                        {
                            ProductTagID = s.ProductTagID,
                            ProductID = s.ProductID,
                            TagID = s.TagID,
                            TagName = t.TagName
                        }).SingleOrDefault();

            return productTag;
        }
    }
}
