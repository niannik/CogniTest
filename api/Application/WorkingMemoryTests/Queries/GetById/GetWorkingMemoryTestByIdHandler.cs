using Application.Common;
using Application.Common.Interfaces;
using Application.WorkingMemoryTests.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryTests.Queries.GetById;

public class GetWorkingMemoryTestByIdHandler : IRequestHandler<GetWorkingMemoryTestByIdQuery, Result<GetWorkingMemoryTestByIdResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryTestByIdHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<GetWorkingMemoryTestByIdResponse>> Handle(GetWorkingMemoryTestByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.WorkingMemoryTests
            .Select(x => new GetWorkingMemoryTestByIdResponse()
            {
                Id = x.Id,
                Description = x.Description,
                AudioPath = x.Audio!.FilePath
            }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (response is null)
            return WorkingMemoryTestErrors.WorkingMemoryTestNotFound;

        return response;
    }
}
