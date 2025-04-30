using Application.Common.Interfaces;
using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public class DatabaseInitializer
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    public DatabaseInitializer(IApplicationDbContext dbContext,  ILogger<DatabaseInitializer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    public virtual async Task CreateDatabaseAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to create database and apply migrations. details: {exceptionMessage}", ex.Message);
            throw;
        }
    }
}