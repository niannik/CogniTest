using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.WorkingMemoryAggregate;

public class WorkingMemoryTest : Entity
{
    public WorkingMemoryTest(WorkingMemoryTestType type, int order)
    {
        Type = type;
        Order = order;
    }

    public WorkingMemoryTestType Type { get; set; }
    public int Order { get; set; }

    public List<WorkingMemoryTerm>? WorkingMemoryTerms { get; set; }
}
