using Application.Common;
using MediatR;

namespace Application.Provinces.Commands.Create;

public record CreateProvinceCommand : IRequest<Result>
{
    public required string Name { get; set; }
}
