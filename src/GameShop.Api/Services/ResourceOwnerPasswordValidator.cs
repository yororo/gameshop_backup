using GameShop.Api.Services.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        #region Declarations

        private IUserRepository _userRepository;
        private IPasswordHashingService _passwordHashingService;

        #endregion Declarations

        #region Constructors

        public ResourceOwnerPasswordValidator(IUserRepository userRepository, IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        #endregion Constructors

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userRepository.FindUserByUsername(context.UserName);

            GrantValidationResult result;

            if (_passwordHashingService.VerifyHashedPassword(user.Account.PasswordHash, context.Password))
            {
                result = new GrantValidationResult(userName, "password");
            }

            result = new GrantValidationResult();
        }

    }
}
}
