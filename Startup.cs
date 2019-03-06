using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using izibongo.api.API.ServiceExtensions;
using izibongo.api.DAL.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;

namespace izibongo.api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env )
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        }
        IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigurSQLServerContext();
            services.ConfigureUserIdentity();
            services.ConfigureRepositoryWrapper();
            services.ConfigurActionContextAccessor();
            services.ConfigureUrlHelper();
            services.ConfigureLoggerService();
            services.AddTransient<DbInitializer>();
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {

                ForwardedHeaders = ForwardedHeaders.All

            });
            app.Use(async (context, next) =>
                {

                    await next();

                    if (context.Response.StatusCode == 404

                        && !Path.HasExtension(context.Request.Path.Value))

                    {

                        context.Request.Path = "/index.html";

                        await next();

                    }

            });
            app.UseHttpsRedirection();
            app.UseMvc();
            seed.Seed().Wait();
        }
    }
}
