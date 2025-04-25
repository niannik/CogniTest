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
    public DbSet<User> Users { get; set; }
    public DbSet<WorkingMemoryTest> WorkingMemoryTests { get; set; }
    public DbSet<WorkingMemoryTerm> WorkingMemoryTerms { get; set; }
    public DbSet<File> Files { get; set; }
}
