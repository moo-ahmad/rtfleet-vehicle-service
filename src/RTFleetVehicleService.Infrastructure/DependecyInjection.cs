using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTFleet.Shared.Common.Dapper;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Infrastructure.Data;
using RTFleetVehicleService.Infrastructure.Messaging;

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

            var rabbitMqSection = configuration.GetSection(RabbitMqOptions.SectionName);
            var rabbitMqOptions = new RabbitMqOptions
            {
                Host = rabbitMqSection["Host"] ?? "localhost",
                VirtualHost = rabbitMqSection["VirtualHost"] ?? "/",
                Username = rabbitMqSection["Username"] ?? "guest",
                Password = rabbitMqSection["Password"] ?? "guest"
            };

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                    o.QueryDelay = TimeSpan.FromSeconds(1);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
