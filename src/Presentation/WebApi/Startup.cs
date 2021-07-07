namespace DeviceManager.WebApi
{
    using DeviceManager.Application;
    using DeviceManager.Application.Common.Exceptions;
    using DeviceManager.Application.Common.Interfaces.Services;
    using DeviceManager.Infrastructure.Persistence;
    using DeviceManager.Infrastructure.Shared;
    using DeviceManager.WebApi.Extensions.StartupExtensions;
    using DeviceManager.WebApi.Services;
    using FluentValidation;
    using Hellang.Middleware.ProblemDetails;
    using Hellang.Middleware.ProblemDetails.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication()
                    .AddInfrastructurePersistence(Configuration)
                    .AddInfrastructureShared();

            services.AddProblemDetails(x =>
            {
                x.Map<NotFoundException>(ex => new StatusCodeProblemDetails(StatusCodes.Status404NotFound));
                x.Map<ValidationException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
                x.Map<BadRequestException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
            });

            services.AddControllers()
                    .AddProblemDetailsConventions()
                    .AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddMetricsExtension()
                    .AddOpenTelemetryExtension(Configuration, WebHostEnvironment)
                    .AddAuthenticationExtension(Configuration)
                    .AddDataProtectionKeysExtension(Configuration)
                    .AddForwardHeadersExtension(Configuration)
                    .AddHealthChecksExtension(Configuration)
                    .AddSwaggerExtension(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            app.UseForwardHeadersExtension(Configuration);

            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMetricsExtension();

            app.UseSwaggerExtension(Configuration);

            app.UseHealthChecksExtension();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                         .RequireAuthorization();
            });
        }
    }
}