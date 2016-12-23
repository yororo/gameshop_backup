using AspNet.Security.OAuth.Validation;
using GameShop.Api.Options;
using GameShop.Api.RequestFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Introspection;

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
            //services.AddGameShopRepositories(Configuration.GetConnectionString("DefaultDatabase"));
                        
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

            // Validate OAuth.
            app.UseOAuthIntrospection(options => 
            {
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                //options.Authority = Configuration["GameShop:Authorization:Authority"];
                options.Authority = "http://localhost:54540";
                options.Audiences.Add("GameShop.Api");
                //options.ClientId = Configuration["GameShop:Authorization:ClientId"];
                options.ClientId = "GameShop.Api";
                //options.ClientSecret = Configuration["GameShop:Authorization:ClientSecret"];
                options.ClientSecret = "secret";
            });

            // CORS
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });

            // NWebsec
            app.UseNWebsec();

            //app.UseOAuthValidation();

            // app.UseJwtBearerAuthentication(new JwtBearerOptions()
            // {
            //     AutomaticAuthenticate = true,
            //     AutomaticChallenge = true,
            //     RequireHttpsMetadata = false,
            //     Audience = "http://localhost:5001/",
            //     Authority = "http://localhost:54540/"
            // });
            
            //app.UseOpenIddict();

            app.UseMvc();

            //Task.Run(async () => await app.Seed());

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
