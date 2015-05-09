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
    [Authorize]
    [RoutePrefix("api/profile")]
    public class UserProfileController : ApiController
    {
        private IUserProfileService userProfileService;
        private IUserRatingService userRatingService;

        public UserProfileController(IUserProfileService userProfileService, IUserRatingService userRatingService)
        {
            this.userProfileService = userProfileService;
            this.userRatingService = userRatingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfileVM"></param>
        /// <returns></returns>
        [Route("update")]
        public HttpResponseMessage PostUserProfile(UserProfileViewModel userProfileVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userProfileVM.UserID = userProfileService.GetUserIDByUserName(User.Identity.Name);
                    userProfileVM.UserName = User.Identity.Name;
                    userProfileService.UpdateUserProfile(userProfileVM);
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
        /// <returns></returns>
        [Route("getprofile")]
        public UserProfileViewModel GetUserProfile()
        {
            return userProfileService.GetUserProfileByUserName(User.Identity.Name);
        }

        [AllowAnonymous]
        [Route("GetUserSummary")]
        public UserProfileViewModel GetUserSummary(string userName)
        {
            var userProfile = userProfileService.GetUserProfileByUserName(userName);
            userProfile.Rating = userRatingService.GetAverageUserRating(userName);

            return userProfile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rating"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [Route("UpdateUserRating")]
        public HttpResponseMessage PostUpdateUserRating(decimal rating, string userName)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userRatingService.InsertUserRating(userName, rating, User.Identity.Name);

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
    }
}
