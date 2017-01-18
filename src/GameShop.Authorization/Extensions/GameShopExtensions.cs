using System.Linq;
using CryptoHelper;
using GameShop.Authorization.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static class GameShopExtensions
    {
        public static IApplicationBuilder SeedOpenIddictApplications(this IApplicationBuilder app)
        {
            using (var context = new ApplicationDbContext(
                app.ApplicationServices.GetRequiredService<DbContextOptions<ApplicationDbContext>>())) {
                context.Database.EnsureCreated();

                var applications = context.Set<OpenIddictApplication>();

                // Add Mvc.Client to the known applications.
                if (!applications.Any()) {
                    // Note: when using the introspection middleware, your resource server
                    // MUST be registered as an OAuth2 client and have valid credentials.
                    //
                    // context.Applications.Add(new OpenIddictApplication {
                    //     Id = "resource_server",
                    //     DisplayName = "Main resource server",
                    //     Secret = Crypto.HashPassword("secret_secret_secret"),
                    //     Type = OpenIddictConstants.ClientTypes.Confidential
                    // });

                    applications.Add(new OpenIddictApplication {
                        ClientId = "GameShop.Web",
                        ClientSecret = Crypto.HashPassword("secret_secret_secret"),
                        DisplayName = "GameShop web application",
                        LogoutRedirectUri = "http://localhost:6000",
                        RedirectUri = "http://localhost:6000/signin-oidc",
                        Type = OpenIddictConstants.ClientTypes.Confidential
                    });

                    // To test this sample with Postman, use the following settings:
                    //
                    // * Authorization URL: http://localhost:54540/connect/authorize
                    // * Access token URL: http://localhost:54540/connect/token
                    // * Client ID: postman
                    // * Client secret: [blank] (not used with public clients)
                    // * Scope: openid email profile roles
                    // * Grant type: authorization code
                    // * Request access token locally: yes
                    applications.Add(new OpenIddictApplication {
                        ClientId = "postman",
                        DisplayName = "Postman",
                        RedirectUri = "https://www.getpostman.com/oauth2/callback",
                        Type = OpenIddictConstants.ClientTypes.Public
                    });

                    context.SaveChanges();
                }
            }

            return app;
        }
    }
}