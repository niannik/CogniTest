using Application.Cities.Common;
using Application.Common;
using Application.Common.Interfaces;
using Application.Provinces.Common;
using Domain.Entities.SchoolAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Schools.Commands.Create;

public class CreateSchoolHandler : IRequestHandler<CreateSchoolCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateSchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
    {
        var province = await _dbContext.Provinces
            .Where(x => x.Id == request.ProvinceId)
            .Select(x => new
            {
                isCityExists = x.Cities!.Any(x => x.Id == request.CityId)
            }).FirstOrDefaultAsync(cancellationToken);

        if (province is null)
            return ProvinceErrors.ProvinceNotFound;

        else if (!province.isCityExists)
            return CitiesErrors.CityNotFound;

        var school = new School(request.Name, request.Address, request.PostalCode, request.TelNumber, request.Level, request.CityId);
        _dbContext.Schools.Add(school);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
