using GameShop.Data.Contracts;
using GameShop.Data.EF;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Entities;
//using GameShop.Data.EF.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameShopRepositories(this IServiceCollection services, string connectionString)
        {
            // Register GameShop context.
            services.AddDbContext<GameShopContext>(options => options.UseSqlServer(connectionString));

            // Register Identity services.
            services.AddIdentity<User, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<GameShopContext, Guid>()
                    .AddDefaultTokenProviders();

            // Register the OpenIddict services, including the default Entity Framework stores.
            services.AddOpenIddict<GameShopContext, Guid>()

            // Enable the authorization, logout, token and userinfo endpoints.
            .EnableAuthorizationEndpoint("/connect/authorize")
            .EnableLogoutEndpoint("/connect/logout")
            .EnableTokenEndpoint("/connect/token")
            .EnableUserinfoEndpoint("/account/userinfo")

            // Note: the Mvc.Client sample only uses the code flow and the password flow, but you
            // can enable the other flows if you need to support implicit or client credentials.
            .AllowAuthorizationCodeFlow()
            .AllowPasswordFlow()
            .AllowRefreshTokenFlow()

            // Make the "client_id" parameter mandatory when sending a token request.
            .RequireClientIdentification()

            // During development, you can disable the HTTPS requirement.
            .DisableHttpsRequirement()

            // When request caching is enabled, authorization and logout requests
            // are stored in the distributed cache by OpenIddict and the user agent
            // is redirected to the same page with a single parameter (request_id).
            // This allows flowing large OpenID Connect requests even when using
            // an external authentication provider like Google, Facebook or Twitter.
            .EnableRequestCaching()

            // Register a new ephemeral key, that is discarded when the application
            // shuts down. Tokens signed using this key are automatically invalidated.
            // This method should only be used during development.
            .AddEphemeralSigningKey();


            // Add repositories.
            //services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
