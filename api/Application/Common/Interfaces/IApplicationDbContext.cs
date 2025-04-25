using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
}
