using GameShop.Api.Options;
using GameShop.Api.RequestFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

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

        /// <summary>
        /// Configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; set; }
        
        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add options.
            services.AddOptions();
            // Add configuration to services.
            services.AddSingleton<IConfiguration>(provider => Configuration);

            services.AddCors();

            //services.AddAuthentication(options => options.SignInScheme = OAuthIntrospectionDefaults.AuthenticationScheme);
            
            // Add gameshop repositories.
            //services.AddGameShopUserAuthorization(Configuration.GetConnectionString("UsersDatabase"));
            services.AddGameShopRepositories(Configuration.GetConnectionString("DefaultDatabase"));
                        
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

            // CORS
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });

            // NWebsec
            //app.UseNWebsec();

            //app.UseOAuthValidation();

            // Alternatively, you can also use the introspection middleware.
            // Using it is recommended if your resource server is in a
            // different application/separated from the authorization server.
            // app.UseOAuthIntrospection(options => {
            //     options.AutomaticAuthenticate = true;
            //     options.AutomaticChallenge = true;
            //     options.Authority = "http://localhost:5000/";
            //     options.Audiences.Add("http://localhost:6001/");
            //     options.ClientId = "GameShop.Api";
            //     options.ClientSecret = "secret_secret_secret";
            //     options.SaveToken = true;
            // });

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Audience = "http://localhost:6001/",
                Authority = "http://localhost:5000/",
                TokenValidationParameters = new TokenValidationParameters(){
                    ValidAudiences = new List<string>(){ "http://localhost:6001/" },
                    ValidIssuers = new List<string>(){ "http://localhost:5000/" }
                },
                SaveToken = true
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
