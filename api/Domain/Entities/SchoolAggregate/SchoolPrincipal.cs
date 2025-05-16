using Domain.Common;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.SchoolAggregate;

public class SchoolPrincipal : AuditableEntity
{
    public SchoolPrincipal(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public int? SchoolId { get; set; }

    public List<UserDevice>? UserDevices { get; set; }
    public School? School { get; set; }
}
