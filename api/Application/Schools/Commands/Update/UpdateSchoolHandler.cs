using Application.Cities.Common;
using Application.Common;
using Application.Common.Interfaces;
using Application.Provinces.Common;
using Application.Schools.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Schools.Commands.Update;

public class UpdateSchoolHandler : IRequestHandler<UpdateSchoolCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public UpdateSchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = await _dbContext.Schools
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (school is null)
            return SchoolErrors.SchoolNotFound;

        var province = await _dbContext.Provinces
            .Where(x => x.Id == request.ProvinceId)
            .Select(x => new
            {
                isCityExists = x.Cities!.Any(x => x.Id == request.CityId)
            }).FirstOrDefaultAsync(cancellationToken);

        if (province is null)
            return ProvinceErrors.ProvinceNotFound;

        else if (!province.isCityExists)
            return CityErrors.CityNotFound;

        school.Name = request.Name;
        school.Address = request.Address;
        school.PostalCode = request.PostalCode;
        school.TelNumber = request.TelNumber;
        school.Level = request.Level;
        school.CityId = request.CityId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
