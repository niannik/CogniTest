using Application.Cities.Common;
using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Schools.Queries.GetAllByCity;

public class GetAllSchoolsByCityHandler : IRequestHandler<GetAllSchoolsByCityQuery, Result<List<GetAllSchoolsByCityResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllSchoolsByCityHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<List<GetAllSchoolsByCityResponse>>> Handle(GetAllSchoolsByCityQuery request, CancellationToken cancellationToken)
    {
        if (request.CityId != null)
        {
            var isCityExists = await _dbContext.Cities.AnyAsync(x => x.Id == request.CityId, cancellationToken);
            if (!isCityExists)
                return CitiesErrors.CityNotFound;
        }
        return await _dbContext.Schools
            .When(request.CityId != null, x => x.CityId == request.CityId)
            .When(request.SearchTerm != null, x => x.Name.Contains(request.SearchTerm!))
            .Select(x => new GetAllSchoolsByCityResponse()
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
