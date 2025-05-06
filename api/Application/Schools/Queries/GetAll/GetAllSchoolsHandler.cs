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
            .Select(x => new GetAllSchoolsResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PostalCode = x.PostalCode,
                TelNumber = x.TelNumber,
                Level = x.Level,
                ProvinceName = x.City.Province!.Name,
                CityName = x.City.Name
            })
            .ToPaginatedListAsync(request.Pagination, cancellationToken);
    }
}
