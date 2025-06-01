using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetBySchool;

public class GetWorkingMemoryResponsesBySchoolHandler : IRequestHandler<GetWorkingMemoryResponsesBySchoolQuery, Result<List<GetWorkingMemoryResponsesBySchoolResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponsesBySchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetWorkingMemoryResponsesBySchoolResponse>>> Handle(GetWorkingMemoryResponsesBySchoolQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.WorkingMemoryResponses
            .Where(x => x.UserTestSessionId == request.UserTestSessionId && x.UserTestSession!.CompletedAt.HasValue)
            .Select(x => new GetWorkingMemoryResponsesBySchoolResponse()
            {
                Oder = x.WorkingMemoryTerm!.Order,
                ResponseTime = x.ResponseTime,
                IsAnswerCorrect = x.IsTarget.HasValue ? x.IsTarget == x.WorkingMemoryTerm.IsTarget : (bool?)null
            }).OrderBy(x => x.Oder)
            .ToListAsync(cancellationToken);

        return response;
    }
}
