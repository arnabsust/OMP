using OMP.Domain.Model;
using OMP.Domain.Repositories;
using OMP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Service
{
    public class SearchService : ISearchService
    {
        private UnitOfWork unitOfWork;
        private Search search;

        public SearchService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="userName"></param>
        public void StoreSearchProduct(int productID, string userName)
        {
            search = (from s in unitOfWork.SearchRepository.Get()
                      where s.ProductID == productID
                      select s).SingleOrDefault();

            if (search != null)
            {
                search.Hit++;
                unitOfWork.SearchRepository.Update(search);
            }
            else
            {
                search = new Search
                {
                    ProductID = productID,
                    SearchDate = DateTime.Now,
                    Hit = 1,
                    UserName = userName
                };

                unitOfWork.SearchRepository.Insert(search);
            }

            unitOfWork.Save();
        }


    }
}
