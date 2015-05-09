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
    public class WishListService : IWishListService
    {
        private UnitOfWork unitOfWork;
        private WishList wishlist;

        public WishListService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CreateWishList(int productID, string userName)
        {
            var result = (from s in unitOfWork.WishListRepository.Get()
                          where s.ProductID == productID && s.CustomerName == userName
                          select s).SingleOrDefault();

            if(result != null )
            {
                return 0;
            }

            wishlist = new WishList
            {
                CustomerName = userName,
                ProductID = productID
            };

            unitOfWork.WishListRepository.Insert(wishlist);
            unitOfWork.Save();

            return wishlist.WishListID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="userName"></param>
        public void DeleteWithList(int productID, string userName)
        {
            unitOfWork.WishListRepository.RawQuery("DELETE FROM WishList WHERE CustomerName='" + userName + "' AND ProductID=" + productID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<CQRS_ProductSummary> GetWishListByUser(string userName)
        {
            var wishList = (from s in unitOfWork.WishListRepository.Get()
                            join cq in unitOfWork.CQRS_ProductSummaryRepository.Get() on s.ProductID equals cq.ProductID
                            where s.CustomerName == userName
                            select cq).ToList();

            return wishList;
        }
    }
}
