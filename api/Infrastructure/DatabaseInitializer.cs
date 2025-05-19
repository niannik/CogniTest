using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Common.Utilities;
using Domain.Entities.AdminAggregate;
using Domain.Entities.CityAggregate;
using Domain.Entities.RoleAggregate;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class DatabaseInitializer
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly AdminSettings _adminSettings;
    public DatabaseInitializer(IApplicationDbContext dbContext,  ILogger<DatabaseInitializer> logger, AdminSettings adminSettings)
    {
        _logger = logger;
        _dbContext = dbContext;
        _adminSettings = adminSettings;
    }

    public virtual async Task CreateDatabaseAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();

            #region Admin Seed Data
            var admins = await _dbContext.Admins.ToListAsync();
            var roles = await _dbContext.Roles.ToListAsync();
            var adminRoles = await _dbContext.AdminRoles.ToListAsync();

            if (!roles.Any(x => x.Name.ToLower() == "superadmin"))
            {
                roles.Add(new Role("SuperAdmin"));
                _dbContext.Roles.AddRange(roles);
            }
            if (!admins.Any(x => x.UserName == _adminSettings.UserName && PasswordHash.VerifyHashedPassword(x.PasswordHash, _adminSettings.Password)))
            {
                var admin = new Admin
                {
                    FullName = "ادمین",
                    UserName = _adminSettings.UserName,
                    PasswordHash = PasswordHash.HashPassword(_adminSettings.Password)
                };
                admins.Add(admin);
                _dbContext.Admins.AddRange(admins);
            }

            await _dbContext.SaveChangesAsync();

            if (adminRoles.Count == 0)
            {
                var adminId = admins.Where(x => x.UserName == _adminSettings.UserName).Select(x => x.Id).First();
                var roleId = roles.Where(x => x.Name.ToLower() == "superadmin").Select(x => x.Id).First();
                var adminRole = new AdminRole(adminId, roleId);
                _dbContext.AdminRoles.Add(adminRole);
                await _dbContext.SaveChangesAsync();
            }
            #endregion

            #region Province Seed Data
            var isProvinceExists = await _dbContext.Provinces.AnyAsync();
            if (!isProvinceExists)
            {
                var province = new Province() { Name = "تهران" };
                _dbContext.Provinces.Add(province);
                await _dbContext.SaveChangesAsync();
                var city = new City("تهران", province.Id);
                _dbContext.Cities.Add(city);
            }
            #endregion

            #region File Seed Data
            var isFileExists = await _dbContext.Files.AnyAsync(default);
            if (!isFileExists)
            {
                var files = FileSeedData.GetFiles();
                _dbContext.Files.AddRange(files);
            }
            #endregion

            #region Working Memory Test Seed Data
            var isWorkingMemoryTestExists = await _dbContext.WorkingMemoryTests.AnyAsync(default);
            if (!isWorkingMemoryTestExists)
            {
                var workingMemoryTests = WorkingMemoryTestSeedData.GetWorkingMemoryTests();
                _dbContext.WorkingMemoryTests.AddRange(workingMemoryTests);
            }
            #endregion

            await _dbContext.SaveChangesAsync();

            #region One Back Seed Data
            var isWorkingMemoryTermExists = await _dbContext.WorkingMemoryTerms.AnyAsync(default);
            if (!isWorkingMemoryTermExists)
            {
                var oneBackTerms = WorkingMemoryTermSeedData.GetOneBackTerms();
                _dbContext.WorkingMemoryTerms.AddRange(oneBackTerms);

                var twoBackTerms = WorkingMemoryTermSeedData.GetTwoBackTerms();
                _dbContext.WorkingMemoryTerms.AddRange(twoBackTerms);

                var threeBackTerms = WorkingMemoryTermSeedData.GetThreeBackTerms();
                _dbContext.WorkingMemoryTerms.AddRange(threeBackTerms);
            }

            #endregion

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to create database and apply migrations. details: {exceptionMessage}", ex.Message);
            throw;
        }
    }
}