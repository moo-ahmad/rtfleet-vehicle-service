using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Application.Interfaces.Auth;
using RTFleetVehicleService.Application.Interfaces.Repository;
using RTFleetVehicleService.Infrastructure.Data;
using RTFleetVehicleService.Infrastructure.Identity;
using RTFleetVehicleService.Infrastructure.Repositories;
using RTFleetVehicleService.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTFleet.Shared.Common.Dapper;

namespace RTFleetVehicleService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDapperRepository>(
                new DapperRepository(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication();

            return services;
        }
    }
}
