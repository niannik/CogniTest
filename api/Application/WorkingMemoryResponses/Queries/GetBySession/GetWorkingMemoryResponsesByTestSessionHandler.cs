using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetBySession;

public class GetWorkingMemoryResponsesByTestSessionHandler : IRequestHandler<GetWorkingMemoryResponsesByTestSessionQuery, Result<List<GetWorkingMemoryResponsesByTestSessionResponse>>>
{
    public readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponsesByTestSessionHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetWorkingMemoryResponsesByTestSessionResponse>>> Handle(GetWorkingMemoryResponsesByTestSessionQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.WorkingMemoryResponses
            .Where(x => x.UserTestSessionId == request.UserTestSessionId && x.UserTestSession!.CompletedAt.HasValue)
            .Select(x => new GetWorkingMemoryResponsesByTestSessionResponse()
            {
                Oder = x.WorkingMemoryTerm!.Order,
                ResponseTime = x.ResponseTime,
                IsAnswerCorrect = x.IsTarget.HasValue ? x.IsTarget == x.WorkingMemoryTerm.IsTarget : (bool?)null
            }).OrderBy(x => x.Oder)
            .ToListAsync(cancellationToken);

        return response;
    }
}
