using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IShopService
    {
        int CreateShop(ShopViewModel shopVM);
        void UpdateShop(ShopViewModel shopVM);
        ShopViewModel GetShopByID(int shopID);
    }
}
