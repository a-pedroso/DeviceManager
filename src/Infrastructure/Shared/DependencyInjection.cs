namespace DeviceManager.Infrastructure.Shared
{
    using DeviceManager.Application.Common.Interfaces.Services;
    using DeviceManager.Infrastructure.Shared.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureShared(this IServiceCollection services)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}