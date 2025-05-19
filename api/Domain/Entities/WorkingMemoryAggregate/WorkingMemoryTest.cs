using Domain.Common;
using Domain.Enums;
using File = Domain.Entities.FileAggregate.File;

namespace Domain.Entities.WorkingMemoryAggregate;

public class WorkingMemoryTest : Entity
{
    public WorkingMemoryTest(WorkingMemoryTestType type, int order, string description)
    {
        Type = type;
        Order = order;
        Description = description;
    }

    public WorkingMemoryTestType Type { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    public int? AudioId { get; set; }

    public File? Audio { get; set; }
    public List<WorkingMemoryTerm>? WorkingMemoryTerms { get; set; }
}
