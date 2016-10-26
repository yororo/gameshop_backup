﻿using System;
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
using IdentityServer4.Services;
using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;

namespace GameShop.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddIdentityServer()
                .AddInMemoryStores()
                .AddInMemoryClients(new List<Client>())
                .AddInMemoryScopes(new List<Scope>())
                .AddInMemoryUsers(new List<InMemoryUser>());

            //Game shop PH data services
            services.UseGameShopRepositories()
                    .UseGameshopSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            services.AddMvcCore(options => 
            {
                //Add action filters.

                //Validated ModelState before executing a controller action.
                options.Filters.Add(typeof(ValidateModelStateActionFilter));

            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseIdentityServer();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
