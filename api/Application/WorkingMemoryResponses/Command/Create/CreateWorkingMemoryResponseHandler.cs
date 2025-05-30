using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using Application.WorkingMemoryTerms.Common;
using Domain.Entities.WorkingMemoryAggregate;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Command.Create;

class CreateWorkingMemoryResponseHandler : IRequestHandler<CreateWorkingMemoryResponseCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateWorkingMemoryResponseHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateWorkingMemoryResponseCommand request, CancellationToken cancellationToken)
    {
        var userTestSession = await _dbContext.UserTestSessions.FirstOrDefaultAsync(x => x.WorkingMemoryTestId == request.TestId && x.UserId == request.UserId);

        if (userTestSession is null)
            return UserTestSessionErrors.UserTestSessionNotFound;

        else if (userTestSession.CompletedAt.HasValue)
            return UserTestSessionErrors.UserTestSessionCompleted;

        var blockNumber = 1;

        if (userTestSession.Status == TestSessionStatus.Block2InProgress)
            blockNumber = 2;

        var workingMemoryTerms = await _dbContext.WorkingMemoryTerms
                .Where(x => x.WorkingMemoryTestId == request.TestId && x.BlockNumber == blockNumber)
                .Select(x => new
                {
                    x.Id,
                    x.Order,
                    x.IsTarget,
                    UserResponse = x.WorkingMemoryResponses!.FirstOrDefault(x => x.UserTestSession!.UserId == request.UserId)
                })
                .OrderBy(x => x.Order)
                .ToListAsync(cancellationToken);

        var currentWorkingMemoryTerm = workingMemoryTerms.FirstOrDefault(x => x.Id == request.TermId);
        if (currentWorkingMemoryTerm is null)
            return WorkingMemoryTermErrors.WorkingMemoryTermNotFound;

        else if (currentWorkingMemoryTerm.UserResponse != null)
            return WorkingMemoryTermErrors.WorkingMemoryTermAlreadyAnswered;

        var userReponse = new WorkingMemoryResponse(request.ResponseTime, request.TermId, userTestSession.Id)
        {
            IsTarget = request.IsTarget
        };

        _dbContext.WorkingMemoryResponses.Add(userReponse);


        var lastBlockTerm = workingMemoryTerms.Last();

        if (lastBlockTerm.Id == request.TermId)
        {
            var targetTermsCount = workingMemoryTerms.Count(x => x.IsTarget);
            var userTargetCounts = workingMemoryTerms.Count(x => x.UserResponse != null && x.UserResponse!.IsTarget.HasValue && x.UserResponse!.IsTarget!.Value && x.IsTarget);

            var targetAccuracy = (double)userTargetCounts / targetTermsCount;

            if (targetAccuracy >= 0.6 && blockNumber == 1)
                userTestSession.Status = TestSessionStatus.Block2InProgress;

            else if (targetAccuracy < 0.6)
            {
                if (blockNumber == 1)
                    userTestSession.Status = TestSessionStatus.Block1Completed;
                else
                    userTestSession.Status = TestSessionStatus.Block2Completed;

                userTestSession.CompletedAt = DateTime.Now;
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
