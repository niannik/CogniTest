using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cities.Queries.GetAll;

public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Result<List<GetAllCitiesResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllCitiesHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetAllCitiesResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Cities
            .AsNoTracking()
            .Select(x => new GetAllCitiesResponse()
            {
                Id = x.Id,
                Name = x.Name,
                ProvinceDetail = new CityProvinceDetail()
                {
                    Id = x.ProvinceId,
                    Name = x.Province!.Name
                }
            }).ToListAsync(cancellationToken);
    }
}
