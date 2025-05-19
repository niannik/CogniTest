using Application.Common;
using Application.Common.Interfaces;
using Application.WorkingMemoryTerms.Common;
using Domain.Entities.WorkingMemoryAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Command;

class CreateWorkingMemoryResponseHandler : IRequestHandler<CreateWorkingMemoryResponseCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateWorkingMemoryResponseHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateWorkingMemoryResponseCommand request, CancellationToken cancellationToken)
    {
        var workingMemoryTerm = await _dbContext.WorkingMemoryTerms
            .Where(x => x.Id == request.TermId && x.WorkingMemoryTestId == request.TestId)
            .Select(x => new
            {
                IsAlreadyAnswered = x.WorkingMemoryResponses!.Any(x => x.StudentId == request.UserId)
            }).FirstOrDefaultAsync(cancellationToken);

        if (workingMemoryTerm is null)
            return WorkingMemoryTermErrors.WorkingMemoryTermNotFound;

        else if (workingMemoryTerm.IsAlreadyAnswered)
            return WorkingMemoryTermErrors.WorkingMemoryTermAlreadyAnswered;

        var userReponse = new WorkingMemoryResponse(request.ResponseTime, request.TermId, request.UserId)
        {
            IsTarget = request.IsTarget
        };

        _dbContext.WorkingMemoryResponses.Add(userReponse);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
