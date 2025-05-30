namespace Application.UserTestSessions.Queries.GetStatus;

public record GetUserTestSessionStatusResponse
{
    public required bool IsCompleted { get; set; }
}
