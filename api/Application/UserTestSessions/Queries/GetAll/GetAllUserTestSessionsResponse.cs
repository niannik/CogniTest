using Domain.Enums;

namespace Application.UserTestSessions.Queries.GetAll;

public record GetAllUserTestSessionsResponse
{
    public required int UserTestSessionId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required int Age { get; set; }
    public required bool IsRightHanded { get; set; }
    public WorkingMemoryTestType TestType { get; set; }
}
