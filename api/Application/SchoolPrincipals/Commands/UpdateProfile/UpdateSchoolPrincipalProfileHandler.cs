using Application.Common;
using Application.Common.Interfaces;
using Application.SchoolPrincipals.Common;
using Application.Schools.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SchoolPrincipals.Commands.UpdateProfile;

public class UpdateSchoolPrincipalProfileHandler : IRequestHandler<UpdateSchoolPrincipalProfileCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public UpdateSchoolPrincipalProfileHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(UpdateSchoolPrincipalProfileCommand request, CancellationToken cancellationToken)
    {
        var schoolPrincipal = await _dbContext.SchoolPrincipals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (schoolPrincipal is null)
            return SchoolPrincipalErrors.SchoolPrincipalNotFound;

        var isSchoolExists = await _dbContext.Schools.AnyAsync(x => x.Id == request.SchoolId, cancellationToken);

        if (!isSchoolExists)
            return SchoolErrors.SchoolNotFound;

        schoolPrincipal.FirstName = request.FirstName;
        schoolPrincipal.LastName = request.LastName;
        schoolPrincipal.NationalCode = request.NationalCode;
        schoolPrincipal.SchoolId = request.SchoolId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
