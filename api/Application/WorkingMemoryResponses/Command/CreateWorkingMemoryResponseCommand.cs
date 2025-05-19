using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Command;

public record CreateWorkingMemoryResponseCommand : IRequest<Result>
{
    public bool? IsTarget { get; set; }
    public long ResponseTime { get; set; }
    public int TestId { get; set; }
    public int TermId { get; set; }
    public int UserId { get; set; }
}
