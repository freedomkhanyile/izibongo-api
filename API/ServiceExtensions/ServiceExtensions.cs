using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Repository.RepositoryWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace izibongo.api.API.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                 builder => builder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .AllowCredentials());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }


        public static void ConfigurSQLServerContext(this IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(ServiceLifetime.Scoped);

        }


        public static void ConfigureUserIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<RepositoryContext>();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
        }

         public static void ConfigurActionContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

       public static void ConfigureUrlHelper(this IServiceCollection services)
        {
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>()
                                           .ActionContext;
                return new UrlHelper(actionContext);
            });
        }
    }
}