using Domain.Common;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.WorkingMemoryAggregate;

public class WorkingMemoryResponse : AuditableEntity
{
    public WorkingMemoryResponse(bool isTarget, long responseTime, int workingMemoryTermId, int userId)
    {
        IsTarget = isTarget;
        ResponseTime = responseTime;
        WorkingMemoryTermId = workingMemoryTermId;
        UserId = userId;
    }
    public bool IsTarget { get; set; }
    public long ResponseTime { get; set; }
    public int WorkingMemoryTermId { get; set; }
    public int UserId { get; set; }

    public WorkingMemoryTerm? WorkingMemoryTerm { get; set; }
    public User? User { get; set; }
}
