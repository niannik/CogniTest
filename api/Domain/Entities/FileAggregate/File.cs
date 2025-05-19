using Domain.Common;
using Domain.Entities.WorkingMemoryAggregate;

namespace Domain.Entities.FileAggregate;

public class File : Entity
{
    public File(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; set; }

    public List<WorkingMemoryTerm>? WorkingMemoryTerms { get; set; }
    public List<WorkingMemoryTest>? WorkingMemoryTests { get; set; }
}
