using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryTerms.Queries.GetStatistics;

public class GetWorkingMemoryTermStatisticsHandler : IRequestHandler<GetWorkingMemoryTermStatisticsQuery, Result<List<GetWorkingMemoryTermStatisticsResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryTermStatisticsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetWorkingMemoryTermStatisticsResponse>>> Handle(GetWorkingMemoryTermStatisticsQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.WorkingMemoryTerms
            .Where(x => x.WorkingMemoryTest!.Type == request.TestType)
            .Select(x => new GetWorkingMemoryTermStatisticsResponse
            {
                Order = x.Order,
                CorrectCount = x.WorkingMemoryResponses!.Count(r => r.UserTestSession!.CompletedAt.HasValue && r.IsTarget.HasValue && r.IsTarget == x.IsTarget),
                IncorrectCount = x.WorkingMemoryResponses!.Count(r => r.UserTestSession!.CompletedAt.HasValue && r.IsTarget.HasValue && r.IsTarget != x.IsTarget),
                UnansweredCount = x.WorkingMemoryResponses!.Count(r => r.UserTestSession!.CompletedAt.HasValue && r.IsTarget == null)
            })
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);

        return response;
    }
}
