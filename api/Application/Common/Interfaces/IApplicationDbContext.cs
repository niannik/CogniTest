using Domain.Entities.AdminAggregate;
using Domain.Entities.CityAggregate;
using Domain.Entities.RoleAggregate;
using Domain.Entities.SchoolAggregate;
using Domain.Entities.UserAggregate;
using Domain.Entities.WorkingMemoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using File = Domain.Entities.FileAggregate.File;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DatabaseFacade Database { get; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
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
