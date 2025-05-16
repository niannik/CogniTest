using Application.Cities.Common;
using Application.Common;
using Application.Common.Interfaces;
using Application.Provinces.Common;
using Domain.Entities.CityAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cities.Commands.Create;

public class CreateCityHandler : IRequestHandler<CreateCityCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateCityHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var province = await _dbContext.Provinces
            .Where(x => x.Id == request.ProvinceId)
            .Select(x => new
            {
                IsCityExists = x.Cities!.Any(x => x.Name == request.Name)
            }).FirstOrDefaultAsync(cancellationToken);

        if (province is null)
            return ProvinceErrors.ProvinceNotFound;

        else if (province.IsCityExists)
            return CityErrors.CityAlreadyExists;

            var city = new City(request.Name, request.ProvinceId);

        _dbContext.Cities.Add(city);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
