using Domain.Common;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.WorkingMemoryAggregate;

public class WorkingMemoryResponse : Entity
{
    public WorkingMemoryResponse(bool isTarget, long responseTime, int workingMemoryTermId, int studentId)
    {
        IsTarget = isTarget;
        ResponseTime = responseTime;
        WorkingMemoryTermId = workingMemoryTermId;
        StudentId = studentId;
    }
    public bool IsTarget { get; set; }
    public long ResponseTime { get; set; }
    public int WorkingMemoryTermId { get; set; }
    public int StudentId { get; set; }

    public WorkingMemoryTerm? WorkingMemoryTerm { get; set; }
    public User? Student { get; set; }
}
