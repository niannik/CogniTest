using Application.Common;
using Application.Common.Interfaces;
using Application.WorkingMemoryTests.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetByTest;

public class GetWorkingMemoryResponsesByTestHandler : IRequestHandler<GetWorkingMemoryResponsesByTestQuery, Result<GetWorkingMemoryResponsesByTestResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponsesByTestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<GetWorkingMemoryResponsesByTestResponse>> Handle(GetWorkingMemoryResponsesByTestQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.WorkingMemoryTests
            .Where(x => x.Id == request.TestId)
            .Select(x => new GetWorkingMemoryResponsesByTestResponse
            {
                CorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                        .Any(r => r.StudentId == request.UserId && r.IsTarget != null && r.IsTarget == t.IsTarget)),
                IncorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                        .Any(r => r.StudentId == request.UserId && r.IsTarget != null && r.IsTarget != t.IsTarget)),
                UnansweredTerms = x.WorkingMemoryTerms!.Count(t => t.Order != 1 && t.WorkingMemoryResponses!
                        .Any(r => r.StudentId == request.UserId && r.IsTarget == null) || !t.WorkingMemoryResponses!.Any(r => r.StudentId == request.UserId)),
                TotalTerms = x.WorkingMemoryTerms!.Count(x => x.Order != 1)
            }).FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return WorkingMemoryTestErrors.WorkingMemoryTestNotFound;

        return response;
    }
}
