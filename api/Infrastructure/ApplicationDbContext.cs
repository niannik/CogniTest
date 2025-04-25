using Application.Common.Interfaces;
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

    public DbSet<User> Users { get; set; }
    public DbSet<WorkingMemoryTest> WorkingMemoryTests { get; set; }
    public DbSet<WorkingMemoryTerm> WorkingMemoryTerms { get; set; }
    public DbSet<File> Files { get; set; }

}
