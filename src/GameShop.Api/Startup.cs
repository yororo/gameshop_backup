using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Data.Repositories;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Providers;
using GameShop.Data.Extensions;
using GameShop.Api.Filters;
using GameShop.Api.Options;
using Microsoft.Extensions.Options;

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
            //services.Configure<Auth0Options>(Configuration.GetSection("Auth0"));
            services.Configure<IdentityServer4Options>(Configuration.GetSection("IdentityServer4"));

            //Game shop PH data services
            services.UseGameShopRepositories()
                    .UseGameshopSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            // Identity Server 4
            services.AddIdentityServer()
                    .AddTemporarySigningCredential()
                    .AddInMemoryScopes(IdentityServer4Options.GetScopes())
                    .AddInMemoryClients(IdentityServer4Options.GetClients())
                    .AddInMemoryUsers(IdentityServer4Options.GetUsers());

            // Add MVC.
            services.AddMvc(options => 
            {
                //Validated ModelState before executing a controller action.
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

            app.UseIdentityServer();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                ScopeName = "api1",

                RequireHttpsMetadata = false
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
