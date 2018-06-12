////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Startup.cs
//
// summary:	Implements the startup class
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NNMetrics.Data;
using NNMetrics.Models;
using NNMetrics.Services;

namespace NNMetrics
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A startup. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Startup
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="configuration">    The configuration. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Configure services. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="services"> The services. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Configures. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="app">  The application. </param>
        /// <param name="env">  The environment. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
