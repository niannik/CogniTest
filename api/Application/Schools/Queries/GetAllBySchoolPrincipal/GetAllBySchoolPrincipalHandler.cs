using Application.Cities.Common;
using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Schools.Queries.GetAllByCity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Schools.Queries.GetAllBySchoolPrincipal;

public class GetAllBySchoolPrincipalHandler : IRequestHandler<GetAllBySchoolPrincipalQuery, Result<List<GetAllBySchoolPrincipalResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllBySchoolPrincipalHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetAllBySchoolPrincipalResponse>>> Handle(GetAllBySchoolPrincipalQuery request, CancellationToken cancellationToken)
    {
        if (request.CityId != null)
        {
            var isCityExists = await _dbContext.Cities.AnyAsync(x => x.Id == request.CityId, cancellationToken);
            if (!isCityExists)
                return CitiesErrors.CityNotFound;
        }
        return await _dbContext.Schools
            .Where(x => x.Principal == null)
            .When(request.CityId != null, x => x.CityId == request.CityId)
            .When(request.SearchTerm != null, x => x.Name.Contains(request.SearchTerm!))
            .Select(x => new GetAllBySchoolPrincipalResponse()
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
