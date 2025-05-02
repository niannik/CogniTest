using Application.Common.Interfaces;
using Domain.Entities.AdminAggregate;
using Domain.Entities.CityAggregate;
using Domain.Entities.RoleAggregate;
using Domain.Entities.SchoolAggregate;
using Domain.Entities.UserAggregate;
using Domain.Entities.WorkingMemoryAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using File = Domain.Entities.FileAggregate.File;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // fix problem of postgresql with DateTime in .Net
        foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
        {
            property.SetColumnType("timestamp without time zone");
        }

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<SchoolPrincipal> SchoolPrincipals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkingMemoryTest> WorkingMemoryTests { get; set; }
    public DbSet<WorkingMemoryTerm> WorkingMemoryTerms { get; set; }
    public DbSet<WorkingMemoryResponse> WorkingMemoryResponses { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
}
