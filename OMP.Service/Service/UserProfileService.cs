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
    public class UserProfileService : IUserProfileService
    {
        private UnitOfWork unitOfWork;
        private UserProfile userProfile;
        public UserProfileService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfileVM"></param>
        public void UpdateUserProfile(UserProfileViewModel userProfileVM)
        {
            userProfile = new UserProfile
            {
                UserID = userProfileVM.UserID,
                UserName = userProfileVM.UserName,
                Email = userProfileVM.Email,
                FullName = userProfileVM.FullName,
                MobileNo = userProfileVM.MobileNo,
                FathersName = userProfileVM.FathersName,
                Address = userProfileVM.Address,
                City = userProfileVM.City,
                Country = userProfileVM.Country,
                ImageUrl = userProfileVM.ImageUrl
            };

            unitOfWork.userProfileRepository.Update(userProfile);
            unitOfWork.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserIDByUserName(string userName) 
        {
            var result = from c in unitOfWork.userProfileRepository.Get()
                         where c.UserName == userName
                         select c.UserID;

            return result.SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserProfileViewModel GetUserProfileByUserName(string userName)
        {
            var result = from c in unitOfWork.userProfileRepository.Get()
                         where c.UserName == userName
                         select new UserProfileViewModel
                         {
                             UserID = c.UserID,
                             UserName = c.UserName,
                             Email = c.Email,
                             FullName = c.FullName,
                             MobileNo = c.MobileNo,
                             FathersName = c.FathersName,
                             Address = c.Address,
                             City = c.City,
                             Country = c.Country,
                             ImageUrl = c.ImageUrl
                         };

            return result.SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="imageURL"></param>
        public void UpdateUserImage(string userName, string imageURL)
        {
            unitOfWork.userProfileRepository.RawQuery("UPDATE UserProfile SET ImageUrl = '" + imageURL + "' WHERE UserName = '" + userName + "'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<UserProfileViewModel> GetAllUserProfile()
        {
            var result = from c in unitOfWork.userProfileRepository.Get()
                         where c.Email.Trim().Length>0
                         select new UserProfileViewModel
                         {
                             UserID = c.UserID,
                             UserName = c.UserName,
                             Email = c.Email,
                             FullName = c.FullName,
                             MobileNo = c.MobileNo,
                             FathersName = c.FathersName,
                             Address = c.Address,
                             City = c.City,
                             Country = c.Country,
                             ImageUrl = c.ImageUrl
                         };

            return result.ToList();
        }
    }
}
