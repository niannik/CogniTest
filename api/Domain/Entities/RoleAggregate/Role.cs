using Domain.Common;

namespace Domain.Entities.RoleAggregate;

public class Role : Entity
{
    public Role(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public List<AdminRole>? AdminRoles { get; set; }
}