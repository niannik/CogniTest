using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Command.Delete;

public class DeleteWorkingMemoryResponseHandler : IRequestHandler<DeleteWorkingMemoryResponseCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteWorkingMemoryResponseHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(DeleteWorkingMemoryResponseCommand request, CancellationToken cancellationToken)
    {
        var workingMemoryResponses = await _dbContext.WorkingMemoryResponses
            .Where(x => x.StudentId == request.UserId && x.WorkingMemoryTerm!.WorkingMemoryTestId == request.TestId)
            .ToListAsync(cancellationToken);

        _dbContext.WorkingMemoryResponses.RemoveRange(workingMemoryResponses);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
