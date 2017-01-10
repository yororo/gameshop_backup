using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GameShop.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace GameShop.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ env.EnvironmentName }.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add options.
            services.AddOptions();

            services.AddSingleton<IConfiguration>(Configuration);

            // Add GameShop options.
            services.Configure<GameShopAuthorizationOptions>(Configuration.GetSection("GameShop:Authorization"));
            services.Configure<GameShopApiOptions>(Configuration.GetSection("GameShop:Api"));

            // Add authentication.
            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IHostingEnvironment env, 
                                ILoggerFactory loggerFactory)
        {
            // Add loggers in pipeline.
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseStaticFiles();

            // Insert a new cookies middleware in the pipeline to store the user
            // identity after he has been redirected from the identity provider.
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AutomaticAuthenticate = true,
                //AutomaticChallenge = true,
                LoginPath = new PathString("/login")
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions {
                // Note: these settings must match the application details
                // inserted in the database at the server level.
                ClientId = "GameShop.Web",
                ClientSecret = "secret_secret_secret",
                PostLogoutRedirectUri = "http://localhost:7000/",

                // Note: setting the Authority allows the OIDC client middleware to automatically
                // retrieve the identity provider's configuration and spare you from setting
                // the different endpoints URIs or the token validation parameters explicitly.
                Authority = "http://localhost:5000/",
                
                RequireHttpsMetadata = false,
                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true,

                // Use the authorization code flow.
                ResponseType = OpenIdConnectResponseType.CodeIdToken,
                AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet,

                Scope = { "email", "roles", "offline_access" }
            });

            // // Alternatively, you can also use the introspection middleware.
            // // Using it is recommended if your resource server is in a
            // // different application/separated from the authorization server.
            // // 
            // app.UseOAuthIntrospection(options => {
            //     options.AutomaticAuthenticate = true;
            //     options.AutomaticChallenge = true;
            //     options.Authority = "http://localhost:5000/";
            //     options.Audiences.Add("http://localhost:7000");
            //     options.ClientId = "GameShop.Web";
            //     options.ClientSecret = "secret_secret_secret";
            //     options.SaveToken = true;
            //     options.AuthenticationScheme = OpenIdConnectDefaults.AuthenticationScheme;
            // });

            

            app.UseMvcWithDefaultRoute();
        }
    }
}
