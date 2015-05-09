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
    //[Authorize]
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryVM"></param>
        /// <returns></returns>
        [Route("Create")]
        public HttpResponseMessage PostCategory(CategoryViewModel categoryVM) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categoryService.CreateCategory(categoryVM);
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
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [Route("GetAllAttributesByID")]
        public List<string> GetAllAttributesByID(int categoryID)
        {
            var atributeList = categoryService.GetAllAttributesByID(categoryID);

            return atributeList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetAllCategory")]
        public List<CategoryViewModel> GetAllCategory()
        {
            var category = categoryService.GetAllCategory();

            return category;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetAllCategoryAnonymous")]
        public List<CategoryViewModel> GetAllCategoryAnonymous()
        {
            var category = categoryService.GetAllCategory();

            return category;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetAllCategoryHierarchical")]
        public List<CategoryHierarchicalViewModel> GetAllCategoryHierarchical()
        {
            var category = categoryService.GetAllCategoryHierarchical();

            return category;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [Route("GetAllSubCategory")]
        public List<CategoryViewModel> GetAllSubCategory(int categoryID)
        {
            var category = categoryService.GetAllSubCategory(categoryID);

            return category;
        }

    }
}
