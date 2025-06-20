using Microsoft.Extensions.DependencyInjection;
using Skyly.Business.Interfaces;
using Skyly.Business.Services;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Skyly.Domain.Entities;

namespace Skyly.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSkylyBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Inject Dapper connection
            services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            // Register services
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IHotelService, Services.HotelService>();
            // Add more services as needed...

            return services;
        }
    }
}
