using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryTerms.Queries.GetAll;

public class GetAllWorkingMemoryTermsHandler : IRequestHandler<GetAllWorkingMemoryTermsQuery, Result<List<GetAllWorkingMemoryTermsResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllWorkingMemoryTermsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetAllWorkingMemoryTermsResponse>>> Handle(GetAllWorkingMemoryTermsQuery request, CancellationToken cancellationToken)
    {
        var userTestSession = await _dbContext.UserTestSessions.FirstOrDefaultAsync(x => x.WorkingMemoryTestId == request.TestId && x.UserId == request.UserId);

        if (userTestSession is null)
            return UserTestSessionErrors.UserTestSessionNotFound;

        else if (userTestSession.CompletedAt.HasValue)
            return UserTestSessionErrors.UserTestSessionCompleted;

        var blockNumber = 1;

        if (userTestSession.Status == TestSessionStatus.Block2InProgress)
            blockNumber = 2;

        return await _dbContext.WorkingMemoryTerms
            .Where(x => x.WorkingMemoryTestId == request.TestId && x.BlockNumber == blockNumber)
            .Select(x => new GetAllWorkingMemoryTermsResponse()
            {
                Id = x.Id,
                Order = x.Order,
                PicturePath = x.Picture!.FilePath,
                UserResponseDetails = x.WorkingMemoryResponses!.Where(x => x.UserTestSession!.UserId == request.UserId)
                    .Select(x => new UserWorkingMemoryTermsResponseDetail()
                    {
                        Id = x.Id,
                        IsTarget = x.IsTarget,
                        ResponseTime = x.ResponseTime
                    }).FirstOrDefault()
            })
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }
}
