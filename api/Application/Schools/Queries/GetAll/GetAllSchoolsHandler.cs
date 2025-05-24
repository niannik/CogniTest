using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Schools.Queries.GetAll;

public class GetAllSchoolsHandler : IRequestHandler<GetAllSchoolsQuery, Result<PaginatedList<GetAllSchoolsResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllSchoolsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PaginatedList<GetAllSchoolsResponse>>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Schools
            .When(request.SearchTerm != null, x => x.Name.Contains(request.SearchTerm!)
                                                   || x.Address.Contains(request.SearchTerm!)
                                                   || x.PostalCode.Contains(request.SearchTerm!)
                                                   || x.TelNumber.Contains(request.SearchTerm!))
            .When(request.CityId != null, x => x.CityId == request.CityId)
            .When(request.Level != null, x => x.Level == request.Level)
            .Select(x => new GetAllSchoolsResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PostalCode = x.PostalCode,
                TelNumber = x.TelNumber,
                Level = x.Level,
                ProvinceDetail = new SchoolProvinceDetail
                {
                    Id = x.City!.ProvinceId,
                    Name = x.City!.Province!.Name
                },
                CityDetail = new SchoolCityDetail
                {
                    Id = x.CityId,
                    Name = x.City!.Name
                }
            })
            .ToPaginatedListAsync(request.Pagination, cancellationToken);
    }
}
