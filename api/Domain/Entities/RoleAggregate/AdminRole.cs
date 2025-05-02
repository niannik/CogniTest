using Domain.Common;
using Domain.Entities.AdminAggregate;

namespace Domain.Entities.RoleAggregate;

public class AdminRole : Entity
{
    public AdminRole(int adminId, int roleId)
    {
        AdminId = adminId;
        RoleId = roleId;
    }

    public int AdminId { get; set; }
    public int RoleId { get; set; }

    public Admin? Admin { get; set; }
    public Role? Role { get; set; }
}