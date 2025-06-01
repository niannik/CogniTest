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
        var userResponseDatails = await _dbContext.UserTestSessions
            .Where(x => x.Id == request.UserTestSessionId && x.User!.SchoolId == request.SchoolId && x.CompletedAt.HasValue)
            .Select(x => new
            {
                UserFullName = x.User!.FirstName + " " + x.User.LastName,
                x.WorkingMemoryTestId,
                CorrectAnswers = (double)x.WorkingMemoryResponses!.Count(x => x.IsTarget.HasValue && x.IsTarget == x.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count(),
            }).FirstOrDefaultAsync(cancellationToken);

        if (userResponseDatails is null)
            return UserTestSessionErrors.UserTestSessionIsNotCompleted;

        var allResponses = await _dbContext.UserTestSessions
            .Include(x => x.User)
            .Include(x => x.WorkingMemoryResponses)!
            .ThenInclude(x => x.WorkingMemoryTerm)
            .Where(x => x.WorkingMemoryTestId == userResponseDatails!.WorkingMemoryTestId && x.CompletedAt.HasValue)
            .ToListAsync(cancellationToken);

        var usersAccuracy = allResponses
            .Select(x => new
            {
                Accuracy = (double)x.WorkingMemoryResponses!.Count(r => r.IsTarget.HasValue && r.IsTarget == r.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count()
            }).ToList();
        var totalAccuracy = (double)usersAccuracy.Sum(x => x.Accuracy) / allResponses.Count() * 100;

        var schoolAccuracy = allResponses
            .Where(x => x.User!.SchoolId == request.SchoolId)
            .Select(x => new
            {
                Accuracy = (double)x.WorkingMemoryResponses!.Count(r => r.IsTarget.HasValue && r.IsTarget == r.WorkingMemoryTerm!.IsTarget) / x.WorkingMemoryResponses!.Count()
            }).ToList();
        var totalSchoolAccuracy = (double)schoolAccuracy.Sum(x => x.Accuracy) / schoolAccuracy.Count * 100;

        return new GetWorkingMemoryResponseAccuracyBySchoolResponse()
        {
            UserFullName = userResponseDatails.UserFullName,
            UserAccuracyPercent = Math.Round(userResponseDatails!.CorrectAnswers * 100),
            TotalSchoolAccuracyPercent = Math.Round(totalSchoolAccuracy),
            TotalAccuracyPercent = Math.Round(totalAccuracy)
        };
    }
}
