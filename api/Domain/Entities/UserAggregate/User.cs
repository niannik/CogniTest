using Domain.Common;
using Domain.Entities.SchoolAggregate;
using Domain.Entities.WorkingMemoryAggregate;
using Domain.Enums;

namespace Domain.Entities.UserAggregate;

public class User : AuditableEntity
{
    public User(string phoneNumber, string firstName, string lastName, int age, Gender gender, bool isRightHanded, int schoolId)
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        IsRightHanded = isRightHanded;
        SchoolId = schoolId;
    }

    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public bool IsRightHanded { get; set; }
    public int SchoolId { get; set; }

    public List<UserDevice>? UserDevices { get; set; }
    public School? School { get; set; }
    public List<UserTestSession>? UserTestSessions { get; set; }
}