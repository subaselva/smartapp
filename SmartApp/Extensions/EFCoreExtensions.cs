using Microsoft.EntityFrameworkCore;
using SmartApp.Infrastructure.Data;

namespace SmartApp.Extensions
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection InjectDbContext(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DbConnection") ?? "")
                       .EnableSensitiveDataLogging()
            );
            return services;
        }   
    }
}
