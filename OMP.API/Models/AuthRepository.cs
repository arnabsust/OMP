using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OMP.Domain.Model;
using OMP.Service.Interface;
using OMP.Service.Service;
using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OMP.API.Models
{
    public class AuthRepository : IDisposable
    {
        private AuthContext db;
        private OMPContext ompContext;
        private UserManager<IdentityUser> userManager;
        private UserProfile userProfile;

        public AuthRepository()
        {
            db = new AuthContext();
            ompContext = new OMPContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
        }

        public async Task<IdentityResult> RegisterUser(UserProfileViewModel userProfileVM)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userProfileVM.UserName,
                Email = userProfileVM.Email
            };

            var result = await userManager.CreateAsync(user, userProfileVM.Password);

            if (result.Succeeded)
            {
                userProfile = new UserProfile
                {
                    UserID = user.Id,
                    UserName = userProfileVM.UserName,
                    Email = userProfileVM.Email,
                    FullName = userProfileVM.FullName,
                    MobileNo = userProfileVM.MobileNo
                };
                ompContext.UserProfile.Add(userProfile);
                ompContext.SaveChanges();
            }
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo) 
        {
            IdentityUser user = await userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                userProfile = new UserProfile
                {
                    UserID = user.Id,
                    UserName = user.UserName
                };
                ompContext.UserProfile.Add(userProfile);
                ompContext.SaveChanges();
            }
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userID, UserLoginInfo user)
        {
            var result = await userManager.AddLoginAsync(userID, user);

            return result;
        }


        public Client FindClient(string clientId)
        {
            var client = db.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = db.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            db.RefreshTokens.Add(token);

            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await db.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                db.RefreshTokens.Remove(refreshToken);
                return await db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            db.RefreshTokens.Remove(refreshToken);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await db.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return db.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
            userManager.Dispose();

        }
    }
}