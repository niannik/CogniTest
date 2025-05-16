using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Provinces.Queries.GetAll;

public class GetAllProvincesHandler : IRequestHandler<GetAllProvincesQuery, Result<List<GetAllProvincesResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllProvincesHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetAllProvincesResponse>>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Provinces
            .When(request.SearchTerm != null, x => x.Name.Contains(request.SearchTerm!))
            .AsNoTracking()
            .Select(x => new GetAllProvincesResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
    }
}
