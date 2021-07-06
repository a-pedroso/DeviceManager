namespace DeviceManager.Infrastructure.Persistence
{
    using DeviceManager.Application.Common.Interfaces.Repositories;
    using DeviceManager.Application.Features.Devices;
    using DeviceManager.Infrastructure.Persistence.Context;
    using DeviceManager.Infrastructure.Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            
            #region Repositories
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
                    .AddScoped<IDeviceRepository, DeviceRepository>();
            #endregion

            return services;
        }
    }
}
