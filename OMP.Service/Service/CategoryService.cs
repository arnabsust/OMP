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
    public class CategoryService : ICategoryService
    {
        private UnitOfWork unitOfWork;
        private Category category;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// This method is used to create Product category.
        /// </summary>
        /// <param name="categoryVM"></param>
        public void CreateCategory(CategoryViewModel categoryVM) 
        {
            category = new Category
            {
                CategoryName = categoryVM.CategoryName,
                ParentCategoryID = categoryVM.ParentCategoryID
            };

            unitOfWork.categoryRepository.Insert(category);
            unitOfWork.Save();
        }

        /// <summary>
        /// This method is used to return product category details information for perticular category.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>Category</returns>
        public Category GetCategoryByID(int categoryID)
        {
            var category = (from s in unitOfWork.categoryRepository.Get()
                            where s.CategoryID == categoryID
                            select s).SingleOrDefault();

            return category;
        }

        /// <summary>
        /// This method returns all product information
        /// </summary>
        /// <returns>List<CategoryViewModel></returns>
        public List<CategoryViewModel> GetAllCategory()
        {
            var categoryList = (from s in unitOfWork.categoryRepository.Get()
                                select new CategoryViewModel 
                                {
                                    CategoryID = s.CategoryID,
                                    CategoryName = s.CategoryName,
                                    ParentCategoryID = s.ParentCategoryID
                                }).ToList();

            return categoryList;
        }

        /// <summary>
        /// This method is used to return all child category of given category ID.
        /// </summary>
        /// <param name="categoryID">categoryID</param>
        /// <returns>List<CategoryViewModel></returns>
        public List<CategoryViewModel> GetAllSubCategory(int categoryID)
        {
            var categoryList = (from s in unitOfWork.categoryRepository.Get()
                                where s.ParentCategoryID == categoryID
                                select new CategoryViewModel
                                {
                                    CategoryID = s.CategoryID,
                                    CategoryName = s.CategoryName,
                                    ParentCategoryID = s.ParentCategoryID
                                }).ToList();

            return categoryList;
        }

        /// <summary>
        /// This method is used to return all available attributes for given category ID.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>List<string></returns>
        public List<string> GetAllAttributesByID(int categoryID)
        {
            var attributeList = (from s in unitOfWork.CategoryAttributesRepository.Get()
                                 where s.CategoryID == categoryID
                                 select s.AttributesName).ToList();

            return attributeList;
        }

        /// <summary>
        /// This method is used to return First Level(Only Parent Category) Category information.
        /// </summary>
        /// <returns>List<CategoryHierarchicalViewModel></returns>
        public List<CategoryHierarchicalViewModel> GetAllCategoryHierarchical()
        {
            var categoryList = (from s in unitOfWork.categoryRepository.Get()
                                      where s.ParentCategoryID == null || s.ParentCategoryID == 0
                                      select new CategoryHierarchicalViewModel
                                      {
                                          CategoryID = s.CategoryID,
                                          CategoryName = s.CategoryName
                                      }).ToList();

            foreach (var item in categoryList)
            {
                var childList = (from s in unitOfWork.categoryRepository.Get()
                                 where s.ParentCategoryID == item.CategoryID
                                 select new CategoryHierarchicalViewModel
                                 {
                                     CategoryID = s.CategoryID,
                                     CategoryName = s.CategoryName
                                 }).ToList();

                item.ChildList = childList;
            }

            return categoryList;
        }

        /// <summary>
        /// This method is used to return all category that certain user has searched in this site.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>List<string></returns>
        public List<string> GetCategoryFromSearch(string userName)
        {
            var categoryList = (from s in unitOfWork.SearchRepository.Get()
                                join c in unitOfWork.CQRS_ProductSummaryRepository.Get() on s.ProductID equals c.ProductID
                                where s.UserName == userName
                                orderby new { s.Hit, s.SearchDate } descending
                                select c.CategoryName).ToList();

            return categoryList;
        }

        /// <summary>
        /// This field returns all category from users product wishlist. 
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns>List<string></returns>
        public List<string> GetCategoryFromWishList(string customerName)
        {
            var categoryList = (from s in unitOfWork.WishListRepository.Get()
                                join c in unitOfWork.CQRS_ProductSummaryRepository.Get() on s.ProductID equals c.ProductID
                                where s.CustomerName == customerName
                                select c.CategoryName).ToList();

            return categoryList;
        }
    }
}
