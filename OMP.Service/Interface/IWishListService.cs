using OMP.Domain.Model;
using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IWishListService
    {
        int CreateWishList(int productID, string userName);
        void DeleteWithList(int productID, string userName);
        List<CQRS_ProductSummary> GetWishListByUser(string userName);
    }
}
