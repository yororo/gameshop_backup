using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GameShop.Data.Contracts;
using Microsoft.Extensions.Options;
using GameShop.Api.RequestFilters;
using GameShop.Api.Services.Interfaces;
using GameShop.Api.Services;

using GameShop.Contracts.Entities;
using GameShop.Api.Contracts.Entities;
using Microsoft.IdentityModel.Tokens;
using GameShop.Api.Options;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Auth0.AuthenticationApi;

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

            if(env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add options.
            services.AddOptions();
            // Add configuration to services.
            services.AddTransient<IConfiguration>(provider => Configuration);
            // Add Auth0 options.
            services.Configure<Auth0Options>(Configuration.GetSection("Auth0"));

            // Add Auth0 services.
            services.AddTransient<IAuthenticationApiClient>(provider => new AuthenticationApiClient(Configuration["Auth0:Domain"]));

            // Add gameshop repositories.
            services.AddGameShopContext(Configuration.GetConnectionString("DefaultConnection"))
                    .AddGameShopRepositories();

            //// Get options from app settings
            //var tokenProviderOptions = Configuration.GetSection(nameof(TokenProviderOptions));

            //// Configure TokenProviderOptions
            //services.Configure<TokenProviderOptions>(options =>
            //{
            //    options.Issuer = tokenProviderOptions[nameof(TokenProviderOptions.Issuer)];
            //    options.Audience = tokenProviderOptions[nameof(TokenProviderOptions.Audience)];
            //    options.SecurityAlgorithm = tokenProviderOptions[nameof(TokenProviderOptions.SecurityAlgorithm)];
            //    options.SigningCredentials = new SigningCredentials(_signingKey, options.SecurityAlgorithm);
            //    options.ValidityInMinutes = double.Parse(tokenProviderOptions[nameof(TokenProviderOptions.ValidityInMinutes)]);
            //});

            //// Add hashing service.
            //services.AddTransient<IPasswordHashingService, PBKDF2PasswordHashingService>();
            //// Add token service.
            //services.AddTransient<ITokenProvider, AccessTokenProvider>();

            // Game shop PH data services
            //services.UseGameShopRepositories()
            //        .UseGameshopSqlServer(Configuration.GetConnectionString("DefaultConnection"));

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<Auth0Options> auth0Options)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Validate JWT tokens.
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Audience = auth0Options.Value.ClientId,
                Authority = $"https://{ auth0Options.Value.Domain }/"
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
