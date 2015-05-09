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
    public class UserRatingService : IUserRatingService
    {
        private UnitOfWork unitOfWork;
        private UserRating userRating;

        public UserRatingService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="rating"></param>
        /// <param name="ratingBy"></param>
        public void InsertUserRating(string userName, decimal rating, string ratingBy)
        {
            userRating = (from s in unitOfWork.UserRatingRepository.Get()
                          where s.UserName == userName && s.RatingBy == ratingBy
                          select s).SingleOrDefault();

            if (userRating == null)
            {
                userRating = new UserRating
                {
                    UserName = userName,
                    Rating = rating,
                    RatingBy = ratingBy
                };

                unitOfWork.UserRatingRepository.Insert(userRating);
            }
            else
            {
                userRating.Rating = rating;
                unitOfWork.UserRatingRepository.Update(userRating);
            }

            unitOfWork.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public decimal GetAverageUserRating(string userName)
        {
            var result = (from s in unitOfWork.UserRatingRepository.Get()
                          where s.UserName == userName
                          select s).ToList();

            if(result != null && result.Count != 0)
            {
                return result.Select(x => x.Rating).Average();
            }

            return 0;
        }
    }
}
