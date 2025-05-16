using Application.Common;
using Application.Common.Interfaces;
using Application.Provinces.Common;
using Domain.Entities.CityAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Provinces.Commands.Create;

public class CreateProvinceHandler : IRequestHandler<CreateProvinceCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateProvinceHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
    {
        var isProvinceExists = await _dbContext.Provinces.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (isProvinceExists)
            return ProvinceErrors.ProvinceAlreadyExists;

        var province = new Province()
        {
            Name = request.Name
        };

        _dbContext.Provinces.Add(province);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
