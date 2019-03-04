using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Repository.RepositoryWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
    }
}