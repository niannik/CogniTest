using Application.Common;
using Application.Common.Interfaces;
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
        return await _dbContext.WorkingMemoryTerms
            .Where(x => x.WorkingMemoryTestId == request.TestId)
            .Select(x => new GetAllWorkingMemoryTermsResponse()
            {
                Id = x.Id,
                Order = x.Order,
                PicturePath = x.Picture!.FilePath,
                UserResponseDetails = x.WorkingMemoryResponses!.Where(x => x.StudentId == request.UserId)
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
