using Application.WorkingMemoryTests.Common.Enums;
using Domain.Enums;

namespace Application.WorkingMemoryTests.Queries.GatAll;

public record GetAllWorkingMemoryTestsResponse
{
    public required int Id { get; set; }
    public required WorkingMemoryTestType Type { get; set; }
    public required int Order { get; set; }
    public WorkingMemoryTestStatus Status { get; set; }
}
