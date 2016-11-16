using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GameShop.Data.Repositories;
using GameShop.Data.Providers;
using Microsoft.Extensions.Options;
using GameShop.Api.Configuration;
using GameShop.Api.RequestFilters;
using GameShop.Api.Services.Interfaces;
using GameShop.Api.Services;

using GameShop.Contracts.Entities;
using GameShop.Api.Contracts.Entities;

namespace GameShop.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add options.
            // services.Configure<Auth0Options>(Configuration.GetSection("Auth0"));
            services.Configure<IdentityServerOptions>(Configuration.GetSection("IdentityServer4"));

            // Add hashing service.
            services.AddTransient<IPasswordHashingService, PBKDF2PasswordHashingService>();

            // Game shop PH data services
            services.UseGameShopRepositories()
                    .UseGameshopSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            // Identity Server 4
            //services.AddIdentityServer()
            //        .AddTemporarySigningCredential()
            //        .AddInMemoryScopes(IdentityServerOptions.GetScopes())
            //        .AddInMemoryClients(IdentityServerOptions.GetClients())
            //        .AddInMemoryUsers(IdentityServerOptions.GetUsers());
           
            services.AddOpenIddict<Client, string, string, string>()
            // Register the ASP.NET Core MVC binder used by OpenIddict.
            // Note: if you don't call this method, you won't be able to
            // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
            .AddMvcBinders()

            // Enable the token endpoint (required to use the password flow).
            .EnableTokenEndpoint("/connect/token")

            // Allow client applications to use the grant_type=password flow.
            .AllowPasswordFlow()

            // During development, you can disable the HTTPS requirement.
            .DisableHttpsRequirement()

            // Register a new ephemeral key, that is discarded when the application
            // shuts down. Tokens signed using this key are automatically invalidated.
            // This method should only be used during development.
            .AddEphemeralSigningKey();

            // Add MVC.
            services.AddMvc(options => 
            {
                // Validate ModelState before executing a controller action.
                options.Filters.Add(typeof(ValidateModelStateActionFilter));
            });

            // Add Swagger.
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseOpenIddict();

            //app.UseIdentityServer();

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = "http://localhost:5000",
            //    ScopeName = "api1",

            //    RequireHttpsMetadata = false
            //});

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
