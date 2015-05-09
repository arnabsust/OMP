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
    public class ShopService : IShopService
    {
        private UnitOfWork unitOfWork;
        private Shop shop;

        public ShopService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopVM"></param>
        /// <returns></returns>
        public int CreateShop(ShopViewModel shopVM)
        {
            shop = new Shop
            {
                UserName = shopVM.UserName,
                ShopName = shopVM.ShopName,
                Address = shopVM.Address,
                Email = shopVM.Email
            };

            unitOfWork.ShopRepository.Insert(shop);
            unitOfWork.Save();

            return shop.ShopID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopVM"></param>
        public void UpdateShop(ShopViewModel shopVM)
        {
            shop = GetShopEntityByID(shopVM.ShopID);

            shop.ShopID = shopVM.ShopID;
            shop.UserName = shopVM.UserName;
            shop.ShopName = shopVM.ShopName;
            shop.Address = shopVM.Address;
            shop.Email = shopVM.Email;

            unitOfWork.ShopRepository.Update(shop);
            unitOfWork.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        private Shop GetShopEntityByID(int shopID)
        {
            shop = (from s in unitOfWork.ShopRepository.Get()
                    where s.ShopID == shopID
                    select s).SingleOrDefault();

            return shop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public ShopViewModel GetShopByID(int shopID)
        {
            var shop = (from s in unitOfWork.ShopRepository.Get()
                        where s.ShopID == shopID
                        select new ShopViewModel 
                        {
                            UserName = s.UserName,
                            ShopName = s.ShopName,
                            Address = s.Address,
                            Email = s.Email,
                            ShopID = s.ShopID
                        }).SingleOrDefault();

            return shop;
        }
    }
}
