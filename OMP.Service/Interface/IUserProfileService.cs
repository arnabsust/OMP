using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IUserProfileService
    {
        void UpdateUserProfile(UserProfileViewModel userProfileVM);
        string GetUserIDByUserName(string userName);
        UserProfileViewModel GetUserProfileByUserName(string userName);
        void UpdateUserImage(string userName, string imageURL);
    }
}
