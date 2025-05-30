using Domain.Common;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.WorkingMemoryAggregate;

public class WorkingMemoryResponse : Entity
{
    public WorkingMemoryResponse(long responseTime, int workingMemoryTermId, int userTestSessionId)
    {
        ResponseTime = responseTime;
        WorkingMemoryTermId = workingMemoryTermId;
        UserTestSessionId = userTestSessionId;
    }
    public bool? IsTarget { get; set; }
    public long ResponseTime { get; set; }

    public int WorkingMemoryTermId { get; set; }
    public int UserTestSessionId { get; set; }

    public WorkingMemoryTerm? WorkingMemoryTerm { get; set; }
    public UserTestSession? UserTestSession { get; set; }
}
