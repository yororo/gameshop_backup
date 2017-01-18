using GameShop.Contracts.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Services.Interfaces
{
    public interface ITokenProvider
    {
        /// <summary>
        /// Generate access token for user.
        /// </summary>
        /// <param name="user">User to generate access token for.</param>
        /// <returns>Access token.</returns>
        Task<string> GenerateTokenAsync(User user);
    }
}
