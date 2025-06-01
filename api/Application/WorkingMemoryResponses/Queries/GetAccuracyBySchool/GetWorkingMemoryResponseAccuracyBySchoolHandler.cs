using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetAccuracyBySchool;

public class GetWorkingMemoryResponseAccuracyBySchoolHandler : IRequestHandler<GetWorkingMemoryResponseAccuracyBySchoolQuery, Result<GetWorkingMemoryResponseAccuracyBySchoolResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponseAccuracyBySchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetWorkingMemoryResponseAccuracyBySchoolResponse>> Handle(GetWorkingMemoryResponseAccuracyBySchoolQuery request, CancellationToken cancellationToken)
    {
        var userTestSessionInfo = await _dbContext.UserTestSessions
            .Where(x => x.Id == request.UserTestSessionId && x.User!.SchoolId == request.SchoolId && x.CompletedAt.HasValue)
            .Select(x => new
            {
                UserFullName = x.User!.FirstName + " " + x.User.LastName,
                x.WorkingMemoryTestId,
                CorrectAnswers = (double)x.WorkingMemoryResponses!.Count(x => x.IsTarget.HasValue && x.IsTarget == x.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count(),
            }).FirstOrDefaultAsync(cancellationToken);

        if (userTestSessionInfo is null)
            return UserTestSessionErrors.UserTestSessionIsNotCompleted;

        var allTestSessions = await _dbContext.UserTestSessions
            .Include(x => x.User)
            .Include(x => x.WorkingMemoryResponses)!
            .ThenInclude(x => x.WorkingMemoryTerm)
            .Where(x => x.WorkingMemoryTestId == userTestSessionInfo!.WorkingMemoryTestId && x.CompletedAt.HasValue)
            .ToListAsync(cancellationToken);

        var allUsersAccuracies = allTestSessions
            .Select(x => new
            {
                Accuracy = (double)x.WorkingMemoryResponses!.Count(r => r.IsTarget.HasValue && r.IsTarget == r.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count()
            }).ToList();
        var averageTestAccuracy = (double)allUsersAccuracies.Sum(x => x.Accuracy) / allTestSessions.Count() * 100;

        var schoolUserAccuracies = allTestSessions
            .Where(x => x.User!.SchoolId == request.SchoolId)
            .Select(x => new
            {
                Accuracy = (double)x.WorkingMemoryResponses!.Count(r => r.IsTarget.HasValue && r.IsTarget == r.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count()
            }).ToList();
        var averageSchoolAccuracy = (double)schoolUserAccuracies.Sum(x => x.Accuracy) / schoolUserAccuracies.Count * 100;

        return new GetWorkingMemoryResponseAccuracyBySchoolResponse()
        {
            UserFullName = userTestSessionInfo.UserFullName,
            UserAccuracyPercent = Math.Round(userTestSessionInfo!.CorrectAnswers * 100),
            TotalSchoolAccuracyPercent = Math.Round(averageSchoolAccuracy),
            TotalAccuracyPercent = Math.Round(averageTestAccuracy)
        };
    }
}
