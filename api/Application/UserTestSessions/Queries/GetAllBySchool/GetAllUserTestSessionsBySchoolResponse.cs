using Domain.Enums;

namespace Application.UserTestSessions.Queries.GetAllBySchool;

public record GetAllUserTestSessionsBySchoolResponse
{
    public required int UserTestSessionId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required int Age { get; set; }
    public required bool IsRightHanded { get; set; }
    public required WorkingMemoryTestType TestType { get; set; }
}
