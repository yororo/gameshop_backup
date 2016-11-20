using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using GameShop.Data.Repositories.Interfaces;
using System.Security.Claims;
using IdentityModel;

namespace GameShop.Api.Services
{
    public class ProfileService : IProfileService
    {
        #region Declarations

        private IUserRepository _userRepository;

        #endregion Declarations

        #region Constructors

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectName = context.Subject.Identity.Name;

            var user = await _userRepository.FindUserByUsername(subjectName);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, $"{ user.UserId }"),
                new Claim(JwtClaimTypes.Name, $"{ user.Profile.Name.FirstName } { user.Profile.Name.LastName }"),
                new Claim(JwtClaimTypes.GivenName, user.Profile.Name.FirstName),
                new Claim(JwtClaimTypes.FamilyName, user.Profile.Name.LastName),
                new Claim(JwtClaimTypes.Email, user.Account.Email),
                new Claim(JwtClaimTypes.EmailVerified, user.Account.EmailVerified.ToString().ToLower(), ClaimValueTypes.Boolean)
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectName = context.Subject.Identity.Name;

            var user = await _userRepository.FindUserByUsername(subjectName);

            if(user != null && user.Account.IsActive)
            {
                context.IsActive = user.Account.IsActive;
            }
        }
    }
}
