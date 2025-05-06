using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Schools.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Schools.Queries.GetById;

public class GetSchoolByIdHandler : IRequestHandler<GetSchoolByIdQuery, Result<GetSchoolByIdResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetSchoolByIdHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetSchoolByIdResponse>> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.Schools
            .Where(x => x.Id == request.Id)
            .Select(x => new GetSchoolByIdResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PostalCode = x.PostalCode,
                TelNumber = x.TelNumber,
                Level = x.Level,
                CityName = x.City!.Name,
                ProvinceName = x.City!.Province!.Name,
                PrincipalDetail = new SchoolPrincipalDetail()
                {
                    Id = x.Principal!.Id,
                    FullName = x.Principal.FirstName + x.Principal.LastName,
                    NationalCode = x.Principal.NationalCode,
                    PhoneNumber = x.Principal.PhoneNumber
                }
            }).FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return SchoolErrors.SchoolNotFound;

        return response;
    }
}
