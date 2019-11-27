using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Web
{
    /// <summary>
    /// Class, which configure the most important parts of ASP NET CORE infrastructure
    /// Normally you should not modify nothing here
    /// </summary>
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // configure localization for the application
            ConfigureLocalization(services);

            // ## ASP NET Core template code
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // ##

            // ## Code added to the template
            ConfigureSwaggerServices(services);

            var serviceProvider = GetServiceProvider(services);

            return serviceProvider;
            // ##
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            // ## ASP NET Core template code
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO check what it is
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseMvc();
            // ##

            // ## Code added to the template
            ConfigureSwagger(app, env);
            // ##
        }
    }
}