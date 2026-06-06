using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTFleet.Shared.Common.Dapper;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Infrastructure.Data;

namespace RTFleetVehicleService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<IDapperRepository>(
                new DapperRepository(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
