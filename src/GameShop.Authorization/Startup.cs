using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GameShop.Authorization.Data;
using GameShop.Authorization.Models;
using GameShop.Authorization.Services;
using NWebsec.AspNetCore.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using GameShop.Authorization.Options;

namespace GameShop.Authorization
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ env.EnvironmentName }.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = configuration.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<GameShopOptions>(Configuration.GetSection("GameShop"));

            services.AddCors();
            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict();
            });

            // Register the Identity services.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Register the OpenIddict services, including the default Entity Framework stores.
            services.AddOpenIddict()
                .AddEntityFrameworkCoreStores<ApplicationDbContext>()
                // Enable the authorization, logout, token and userinfo endpoints.
                .EnableAuthorizationEndpoint("/connect/authorize")
                .EnableLogoutEndpoint("/connect/logout")
                .EnableUserinfoEndpoint("/account/userinfo")
                .EnableTokenEndpoint("/connect/token")
                .EnableIntrospectionEndpoint("/connect/introspect")
                .UseJsonWebTokens()

                // Note: the Mvc.Client sample only uses the code flow and the password flow, but you
                // can enable the other flows if you need to support implicit or client credentials.
                .AllowAuthorizationCodeFlow()
                .AllowImplicitFlow()
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

            // On production, using a X.509 certificate stored in the machine store is recommended.
            // You can generate a self-signed certificate using Pluralsight's self-cert utility:
            // https://s3.amazonaws.com/pluralsight-free/keith-brown/samples/SelfCert.zip
            // 
            // services.AddOpenIddict<ApplicationDbContext>()
            //     .AddSigningCertificate("7D2A741FE34CC2C7369237A5F2078988E17A6A75");
            // 
            // Alternatively, you can also store the certificate as an embedded .pfx resource
            // directly in this assembly or in a file published alongside this project:
            // 
            // services.AddOpenIddict<ApplicationDbContext>()
            //     .AddSigningCertificate(
            //          assembly: typeof(Startup).GetTypeInfo().Assembly,
            //          resource: "Mvc.Server.Certificate.pfx",
            //          password: "OpenIddict");

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            // CORS
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });
            
            // Add a middleware used to validate access
            // tokens and protect the API endpoints.
            app.UseOAuthValidation();

            // Alternatively, you can also use the introspection middleware.
            // Using it is recommended if your resource server is in a
            // different application/separated from the authorization server.
            // app.UseOAuthIntrospection(options => 
            // {
            //     options.AutomaticAuthenticate = true;
            //     options.AutomaticChallenge = true;
            //     options.Authority = "http://localhost:54540/";
            //     options.Audiences.Add("http://localhost:5001");
            //     options.ClientId = "GameShop.Api";
            //     options.ClientSecret = "api_secret";
            // });

            // app.UseCsp(options => options.DefaultSources(directive => directive.Self())
            //     .ImageSources(directive => directive.Self()
            //         .CustomSources("*"))
            //     .ScriptSources(directive => directive.Self()
            //         .UnsafeInline())
            //     .StyleSources(directive => directive.Self()
            //         .UnsafeInline()));

            // app.UseXContentTypeOptions();

            // app.UseXfo(options => options.Deny());

            // app.UseXXssProtection(options => options.EnabledWithBlockMode());

            // app.UseGoogleAuthentication(new GoogleOptions {
            //     ClientId = "560027070069-37ldt4kfuohhu3m495hk2j4pjp92d382.apps.googleusercontent.com",
            //     ClientSecret = "n2Q-GEw9RQjzcRbU3qhfTj8f"
            // });

            // app.UseTwitterAuthentication(new TwitterOptions {
            //     ConsumerKey = "6XaCTaLbMqfj6ww3zvZ5g",
            //     ConsumerSecret = "Il2eFzGIrYhz6BWjYhVXBPQSfZuS4xoHpSSyD9PI"
            // });

            //app.UseStatusCodePagesWithReExecute("/error");

            //app.UseIdentity();

            app.UseOpenIddict();

            app.UseMvcWithDefaultRoute();

            // Seed
            app.SeedOpenIddictApplications();
        }
    }
}
