using Domain.Common;
using File = Domain.Entities.FileAggregate.File;

namespace Domain.Entities.WorkingMemoryAggregate;

public class WorkingMemoryTerm : Entity
{
    public WorkingMemoryTerm(int order, bool isTarget, int blockNumber, int workingMemoryTestId, int pictureId)
    {
        Order = order;
        IsTarget = isTarget;
        BlockNumber = blockNumber;
        WorkingMemoryTestId = workingMemoryTestId;
        PictureId = pictureId;
    }

    public int Order { get; set; }
    public bool IsTarget { get; set; }
    public int BlockNumber { get; set; }
    public int WorkingMemoryTestId { get; set; }
    public int PictureId { get; set; }

    public WorkingMemoryTest? WorkingMemoryTest { get; set; }
    public File? Picture { get; set; }
    public List<WorkingMemoryResponse>? WorkingMemoryResponses { get; set; }
}
