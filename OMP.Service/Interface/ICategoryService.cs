using OMP.Domain.Model;
using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface ICategoryService
    {
        void CreateCategory(CategoryViewModel categoryVM);
        Category GetCategoryByID(int categoryID);
        List<string> GetAllAttributesByID(int categoryID);
        List<CategoryViewModel> GetAllCategory();
        List<CategoryHierarchicalViewModel> GetAllCategoryHierarchical();
        List<CategoryViewModel> GetAllSubCategory(int categoryID);
        List<string> GetCategoryFromSearch(string userName);
        List<string> GetCategoryFromWishList(string customerName);
    }
}
