using Infrastructure.Configuration;
using Infrastructure.Persistence;

namespace NotesAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfiguration>(
                configuration.GetSection(DatabaseConfiguration.NotesDatabase));

            return services;
        }

        public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            return services;
        }
    }
}
