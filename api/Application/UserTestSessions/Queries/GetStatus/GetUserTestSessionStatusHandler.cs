using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserTestSessions.Queries.GetStatus;

public class GetUserTestSessionStatusHandler : IRequestHandler<GetUserTestSessionStatusQuery, Result<GetUserTestSessionStatusResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetUserTestSessionStatusHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetUserTestSessionStatusResponse>> Handle(GetUserTestSessionStatusQuery request, CancellationToken cancellationToken)
    {
        var userTestSessionStatus = await _dbContext.UserTestSessions
            .Where(x => x.WorkingMemoryTestId == request.TestId && x.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (userTestSessionStatus is null)
            return UserTestSessionErrors.UserTestSessionNotFound;

        var response = new GetUserTestSessionStatusResponse() { IsCompleted = false };
        if (userTestSessionStatus.CompletedAt.HasValue)
            response.IsCompleted = true;

        return response;
    }
}
