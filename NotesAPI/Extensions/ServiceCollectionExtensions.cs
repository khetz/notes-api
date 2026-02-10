using Application.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Respositories;
using Infrastructure.Security;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var databaseConfig = sp.GetRequiredService<IOptions<DatabaseConfiguration>>().Value;
                options.UseSqlServer(databaseConfig.ConnectionString);
            });

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(JwtSettings.SectionName);
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey  = new SymmetricSecurityKey(secretKey)
                };
            });

            return services;
        }
    }
}
