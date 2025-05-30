using Application.Common;
using Application.Common.Interfaces;
using Application.UserTestSessions.Common;
using Domain.Entities.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserTestSessions.Commands.Create;

public class CreateUserTestSessionHandler : IRequestHandler<CreateUserTestSessionCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateUserTestSessionHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateUserTestSessionCommand request, CancellationToken cancellationToken)
    {
        var userSessions = await _dbContext.UserTestSessions
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        var isUserTestSessionExists = userSessions.Any(x => x.WorkingMemoryTestId == request.TestId);
        if (isUserTestSessionExists)
            return UserTestSessionErrors.UserTestSessionAlreadyExists;

        var isUserSessionActive = userSessions.Any(x => x.CompletedAt == null);
        if (isUserSessionActive)
            return UserTestSessionErrors.UserAlreadyHasActiveTestSession;


        var userTestSession = new UserTestSession(request.UserId, request.TestId);
        _dbContext.UserTestSessions.Add(userTestSession);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
