using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserTestSessions.Commands.Delete;

public class DeleteUserTestSessionHandler : IRequestHandler<DeleteUserTestSessionCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteUserTestSessionHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(DeleteUserTestSessionCommand request, CancellationToken cancellationToken)
    {
        var userTestSession = await _dbContext.UserTestSessions
            .Where(x => x.UserId == request.UserId && x.WorkingMemoryTestId == request.TestId)
            .FirstOrDefaultAsync(cancellationToken);

        if (userTestSession is null)
            return UserTestSessionErrors.UserTestSessionNotFound;
        else if (userTestSession.CompletedAt.HasValue)
            return UserTestSessionErrors.CannotCancelCompletedTestSession;

        _dbContext.UserTestSessions.Remove(userTestSession);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
