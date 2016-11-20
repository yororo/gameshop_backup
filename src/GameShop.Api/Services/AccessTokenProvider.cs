using GameShop.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Contracts.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using GameShop.Api.Options;
using Microsoft.Extensions.Options;

namespace GameShop.Api.Services
{
    public class AccessTokenProvider : ITokenProvider
    {
        #region Declarations

        private IUserRepository _userRepository;
        private IPasswordHashingService _passwordHashingService;
        private TokenProviderOptions _tokenProviderOptions;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userRepository">User repository.</param>
        /// <param name="passwordHashingService">Password hashing service.</param>
        /// <param name="tokenProviderOptions">Token provider options.</param>
        public AccessTokenProvider(IUserRepository userRepository,
                                        IPasswordHashingService passwordHashingService,
                                        IOptions<TokenProviderOptions> tokenProviderOptions)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _tokenProviderOptions = tokenProviderOptions.Value;
        }

        #endregion Constructors

        #region IAccessTokenProvider Implementation
        
        /// <summary>
        /// Generate JWT access token for user.
        /// </summary>
        /// <param name="user">User to generate access token for.</param>
        /// <returns>JWT access token.</returns>
        public async Task<string> GenerateTokenAsync(User user)
        {
            var currentDate = DateTime.UtcNow;

            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Account.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Account.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Profile.Name.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Profile.Name.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenProviderOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, currentDate.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp, currentDate.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken
            (
                issuer: _tokenProviderOptions.Issuer,
                audience: _tokenProviderOptions.Audience,
                claims: claims,
                notBefore: currentDate,
                expires: currentDate.Add(_tokenProviderOptions.ValidUntil),
                signingCredentials: _tokenProviderOptions.SigningCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(jwt);
        }

        #endregion IAccessTokenProvider Implementation
    }
}
