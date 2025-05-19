using Application.Common;
using Application.Common.Interfaces;
using Application.WorkingMemoryTests.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetByTestId;

public class GetWorkingMemoryResponsesByTestIdHandler : IRequestHandler<GetWorkingMemoryResponsesByTestIdQuery, Result<GetWorkingMemoryResponsesByTestIdResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponsesByTestIdHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<GetWorkingMemoryResponsesByTestIdResponse>> Handle(GetWorkingMemoryResponsesByTestIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.WorkingMemoryTests
            .Where(x => x.Id == request.TestId)
            .Select(x => new GetWorkingMemoryResponsesByTestIdResponse
            {
                CorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                        .Any(r => r.StudentId == request.UserId && r.IsTarget != null && r.IsTarget == t.IsTarget)),
                IncorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                        .Any(r => r.StudentId == request.UserId && r.IsTarget != null && r.IsTarget != t.IsTarget)),
                UnansweredTerms = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                        .Any(r => r.StudentId == request.UserId && r.IsTarget == null) || !t.WorkingMemoryResponses!.Any(r => r.StudentId == request.UserId)),
                TotalTerms = x.WorkingMemoryTerms!.Count()
            }).FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return WorkingMemoryTestErrors.WorkingMemoryTestNotFound;

        return response;
    }
}
