using System;
using hhs_p6_webshop_project.App.Config;
using hhs_p6_webshop_project.App.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace hhs_p6_webshop_project {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.secret.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment()) {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Program.FileConfig = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //Initialize configuration options
            // Adds services required for using options.
            services.AddOptions();

            // Configure with Microsoft.Extensions.Options.ConfigurationExtensions
            // Binding the whole configuration should be rare, subsections are more typical.
            services.Configure<SecureAppConfig>(Program.FileConfig);

            // Get the database connection string, and check whether it's remote
            String dbConStr = DbUtils.GetAndInjectConnectionString();
            bool isRemote = DbUtils.IsRemote();
            
            // Add framework services for local or remote databases
            if(isRemote)
                services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(dbConStr));
            else
                services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlite(dbConStr));

            // Print the selected database
            Console.WriteLine("Selected database: " + dbConStr);
            Console.WriteLine("Is remote database: " + (isRemote ? "Yes" : "No"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //Add support for SparkPost Transactional Email Service
            //services.AddSparkPost();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IProductService, ProductService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext context, IServiceProvider serviceProvider, IOptions<SecureAppConfig> secureConfig) {

            //Save reference to the mail client
            //MailClient.Client = serviceProvider.GetService<SparkPostClient>();

            loggerFactory.AddConsole(Program.FileConfig.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else {
                app.UseStatusCodePagesWithRedirects("/Home/Error");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseGoogleAuthentication(new GoogleOptions() {
                ClientId = secureConfig.Value.GoogleClientId,
                ClientSecret = secureConfig.Value.GoogleClientSecret
            });


            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


#if DEBUG
            // Seed database if not running in production
            if (Program.AppConfig.DatabaseReset)
                DbBuilder.Rebuild(context);
#endif
        }
    }
}