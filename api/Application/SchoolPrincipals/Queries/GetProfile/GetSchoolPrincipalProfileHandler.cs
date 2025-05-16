using Application.Common;
using Application.Common.Interfaces;
using Application.SchoolPrincipals.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SchoolPrincipals.Queries.GetProfile;

public class GetSchoolPrincipalProfileHandler : IRequestHandler<GetSchoolPrincipalProfileQuery, Result<GetSchoolPrincipalProfileResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetSchoolPrincipalProfileHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetSchoolPrincipalProfileResponse>> Handle(GetSchoolPrincipalProfileQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.SchoolPrincipals
            .Where(x => x.Id == request.Id)
            .Select(x => new GetSchoolPrincipalProfileResponse()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalCode = x.NationalCode,
                PhoneNumber = x.PhoneNumber,
                SchoolDetail = new SchoolPrincipalSchoolDetail
                {
                    Id = x.SchoolId,
                    Name = x.School!.Name
                }
            }).FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return SchoolPrincipalErrors.SchoolPrincipalNotFound;

        return response;
    }
}
