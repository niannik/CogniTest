using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using Application.WorkingMemoryTests.Common;
using Domain.Enums;
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
        var response = new GetWorkingMemoryResponsesByTestResponse
        {
            CorrectAnswers = 0,
            IncorrectAnswers = 0,
            TotalTerms = 0,
            UnansweredTerms = 0
        };
        var userTestSession = await _dbContext.UserTestSessions.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.WorkingMemoryTestId == request.TestId, cancellationToken);
        var blockNumber = 1;
        if (userTestSession is null)
            return UserTestSessionErrors.UserTestSessionNotFound;
        else if (userTestSession.Status == TestSessionStatus.Block1InProgress || userTestSession.Status == TestSessionStatus.Block2InProgress)
            return UserTestSessionErrors.TestResultUnavailableForActiveSession;
        else if (userTestSession.Status == TestSessionStatus.Block1Completed)
        {
            response = await _dbContext.WorkingMemoryTests
                .Where(x => x.Id == request.TestId)
                .Select(x => new GetWorkingMemoryResponsesByTestResponse
                {
                    CorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.BlockNumber == blockNumber && t.WorkingMemoryResponses!
                            .Any(r => r.UserTestSession!.UserId == request.UserId && r.IsTarget != null && r.IsTarget == t.IsTarget)),
                    IncorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.BlockNumber == blockNumber && t.WorkingMemoryResponses!
                            .Any(r => r.UserTestSession!.UserId == request.UserId && r.IsTarget != null && r.IsTarget != t.IsTarget)),
                    UnansweredTerms = x.WorkingMemoryTerms!.Count(t => t.BlockNumber == blockNumber && t.WorkingMemoryResponses!
                            .Any(r => r.UserTestSession!.UserId == request.UserId && r.IsTarget == null)),
                    TotalTerms = x.WorkingMemoryTerms!.Count(x => x.BlockNumber == blockNumber)
                }).FirstOrDefaultAsync(cancellationToken);

            if (response is null)
                return WorkingMemoryTestErrors.WorkingMemoryTestNotFound;
        }

        else
        {
            response = await _dbContext.WorkingMemoryTests
                .Where(x => x.Id == request.TestId)
                .Select(x => new GetWorkingMemoryResponsesByTestResponse
                {
                    CorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                            .Any(r => r.UserTestSession!.UserId == request.UserId && r.IsTarget != null && r.IsTarget == t.IsTarget)),
                    IncorrectAnswers = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                            .Any(r => r.UserTestSession!.UserId == request.UserId && r.IsTarget != null && r.IsTarget != t.IsTarget)),
                    UnansweredTerms = x.WorkingMemoryTerms!.Count(t => t.WorkingMemoryResponses!
                            .Any(r => r.UserTestSession!.UserId == request.UserId && r.IsTarget == null) || !t.WorkingMemoryResponses!.Any(r => r.UserTestSession!.UserId == request.UserId)),
                    TotalTerms = x.WorkingMemoryTerms!.Count()
                }).FirstOrDefaultAsync(cancellationToken);

            if (response is null)
                return WorkingMemoryTestErrors.WorkingMemoryTestNotFound;
        }


        return response;
    }
}