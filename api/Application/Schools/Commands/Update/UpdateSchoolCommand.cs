using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Schools.Commands.Update;

public record UpdateSchoolCommand : IRequest<Result>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public required string TelNumber { get; set; }
    public required SchoolLevel Level { get; set; }
    public required int ProvinceId { get; set; }
    public required int CityId { get; set; }
}
