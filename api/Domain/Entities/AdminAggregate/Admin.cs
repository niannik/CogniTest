using Domain.Common;
using Domain.Entities.RoleAggregate;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.AdminAggregate;

public class Admin : AuditableEntity
{
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    public List<UserDevice>? UserDevices { get; set; }
    public List<AdminRole>? AdminRoles { get; set; }
}
