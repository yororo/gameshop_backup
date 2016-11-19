using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Auth0.AuthenticationApi;
using Auth0.ManagementApi;
using GameShop.Api.Auth.Options;

namespace GameShop.Api.Auth
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
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add configuration to services.
            services.AddTransient<IConfiguration>(provider => Configuration);

            // Add auth0 services.
            services.AddTransient<IAuthenticationApiClient>(provider => new AuthenticationApiClient(Configuration["Auth0:Domain"]));

            // Add framework services.
            services.AddMvc();

            // Add swagger.
            services.AddSwaggerGen();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add the Auth0 Settings object so it can be injected
            services.Configure<Auth0Options>(Configuration.GetSection("Auth0"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<Auth0Options> auth0Settings)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Audience = auth0Settings.Value.ClientId,
                Authority = $"https://{ auth0Settings.Value.Domain }/"
            });

            app.UseMvc();

            // Use swagger.
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
