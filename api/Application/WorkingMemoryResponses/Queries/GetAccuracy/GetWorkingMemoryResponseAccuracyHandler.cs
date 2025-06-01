using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetAccuracy;

public class GetWorkingMemoryResponseAccuracyHandler : IRequestHandler<GetWorkingMemoryResponseAccuracyQuery, Result<GetWorkingMemoryResponseAccuracyResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponseAccuracyHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetWorkingMemoryResponseAccuracyResponse>> Handle(GetWorkingMemoryResponseAccuracyQuery request, CancellationToken cancellationToken)
    {
        var userResponseDatails = await _dbContext.UserTestSessions
            .Where(x => x.Id == request.UserTestSessionId && x.CompletedAt.HasValue)
            .Select(x => new
            {
                UserFullName = x.User!.FirstName + " " + x.User.LastName,
                x.WorkingMemoryTestId,
                CorrectAnswers = (double) x.WorkingMemoryResponses!.Count(x => x.IsTarget.HasValue && x.IsTarget == x.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count(),
            }).FirstOrDefaultAsync(cancellationToken);

        if (userResponseDatails is null)
            return UserTestSessionErrors.UserTestSessionIsNotCompleted;

        var responsesDetails = await _dbContext.UserTestSessions
            .Where(x => x.WorkingMemoryTestId == userResponseDatails!.WorkingMemoryTestId && x.CompletedAt.HasValue)
            .Select(x => new
            {
                Accuracy = (double)x.WorkingMemoryResponses!.Count(r => r.IsTarget.HasValue && r.IsTarget == r.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count()
            })
            .ToListAsync(cancellationToken);

        var totalAccuracy = (double)responsesDetails.Sum(x => x.Accuracy) / responsesDetails.Count() * 100;

        return new GetWorkingMemoryResponseAccuracyResponse()
        {
            UserFullName = userResponseDatails.UserFullName,
            UserAccuracyPercent = Math.Round(userResponseDatails!.CorrectAnswers * 100),
            TotalAccuracyPercent = Math.Round(totalAccuracy)
        };

    }
}