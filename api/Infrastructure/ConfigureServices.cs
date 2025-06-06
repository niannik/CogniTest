﻿using Application.Common.Interfaces;
using Application.Common.Settings;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        ArgumentNullException.ThrowIfNull(nameof(connectionString));

        services.AddScoped<AppSaveChangeInterceptor>();

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();
        services.AddSingleton(dataSource);


        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(serviceProvider.GetRequiredService<NpgsqlDataSource>());

            options.AddInterceptors(serviceProvider.GetRequiredService<AppSaveChangeInterceptor>());
        });

        services.AddMemoryCache();

        services.AddScoped<DatabaseInitializer>();

        services.AddScoped<TokenValidator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<TokenFactoryService>();
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddScoped<ISignInAdminService, SignInAdminService>();
        services.AddScoped<ISignInUserService, SignInUserService>();
        services.AddScoped<ISignInSchoolPrincipalService, SignInSchoolPrincipalService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<IFileManager, FileManager>();

        services.AddSingleton(_ =>
        {
            BearerTokenSettings bearerTokenSettings = new();
            configuration.GetRequiredSection("BearerTokenSettings").Bind(bearerTokenSettings);

            ArgumentException.ThrowIfNullOrEmpty(bearerTokenSettings.SecretKey, nameof(bearerTokenSettings.SecretKey));

            return bearerTokenSettings;
        });
        services.AddSingleton(_ =>
        {
            AdminSettings adminSettings = new();
            configuration.GetRequiredSection("AdminSettings").Bind(adminSettings);
            return adminSettings;
        });
        services.AddSingleton(_ =>
        {
            SmsSettings smsSettings = new();
            configuration.GetRequiredSection("SmsSettings").Bind(smsSettings);
            return smsSettings;
        });
    }
}