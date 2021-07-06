﻿using DeviceManager.Application.Common.Interfaces.Services;
using DeviceManager.Infrastructure.Persistence.Context;
using DeviceManager.WebApi;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[SetUpFixture]
public class Testing
{   
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
    private static string _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _currentUserId = "mockuserid";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        var services = new ServiceCollection();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "DeviceManager.WebApi"));

        services.AddLogging();

        var startup = new Startup(_configuration, Mock.Of<IWebHostEnvironment>(w =>
                                                            w.EnvironmentName == "Development" &&
                                                            w.ApplicationName == "DeviceManager.WebApi"));

        startup.ConfigureServices(services);

        // Replace service registration for ICurrentUserService
        // Remove existing registration
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUserService));
        services.Remove(currentUserServiceDescriptor);
        // Register testing version
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(s => s.UserId).Returns(_currentUserId);
        services.AddTransient<ICurrentUserService>(provider => currentUserServiceMock.Object);

        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<IMediator>();

        return await mediator.Send(request);
    }

    public static string RunAsDefaultUserAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var userSvc = scope.ServiceProvider.GetService<ICurrentUserService>();

        return userSvc.UserId;
    }

    public static Task ResetState()
    {
        _currentUserId = null;

        return Task.CompletedTask;
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] id)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(id);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // Method intentionally left empty.
    }
}
