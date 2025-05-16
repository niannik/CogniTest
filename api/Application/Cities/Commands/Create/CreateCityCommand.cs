using Application.Common;
using MediatR;

namespace Application.Cities.Commands.Create;

public class CreateCityCommand : IRequest<Result>
{
    public required string Name { get; set; }
    public required int ProvinceId { get; set; }
}
