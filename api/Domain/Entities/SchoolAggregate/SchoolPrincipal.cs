using Domain.Common;

namespace Domain.Entities.SchoolAggregate;

public class SchoolPrincipal : AuditableEntity
{
    public SchoolPrincipal(string phoneNumber, string nationalCode, string firstName, string lastName, int schoolId)
    {
        PhoneNumber = phoneNumber;
        NationalCode = nationalCode;
        FirstName = firstName;
        LastName = lastName;
        SchoolId = schoolId;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string NationalCode { get; set; }
    public int SchoolId { get; set; }

    public School? School { get; set; }
}
