using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace NotesAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfiguration>(
                configuration.GetSection(DatabaseConfiguration.NotesDatabase));

            services.Configure<JwtSettings>(
                configuration.GetSection(JwtSettings.SectionName));

            return services;
        }

        public static IServiceCollection RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(DatabaseConfiguration.NotesDatabase)));
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();

            return services;
        }
    }
}
