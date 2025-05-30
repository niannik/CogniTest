using Domain.Common;
using Domain.Entities.WorkingMemoryAggregate;
using Domain.Enums;

namespace Domain.Entities.UserAggregate;

public class UserTestSession : AuditableEntity
{
    public UserTestSession(int userId, int workingMemoryTestId)
    {
        UserId = userId;
        WorkingMemoryTestId = workingMemoryTestId;
        Status = TestSessionStatus.Block1InProgress;
    }
    public DateTime? CompletedAt { get; set; }
    public TestSessionStatus Status { get; set; }

    public int UserId { get; set; }
    public int WorkingMemoryTestId { get; set; }

    public User? User { get; set; }
    public WorkingMemoryTest? WorkingMemoryTest { get; set; }
    public List<WorkingMemoryResponse>? WorkingMemoryResponses { get; set; }
}
