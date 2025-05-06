using Domain.Enums;

namespace Application.Schools.Commands.Update;

public record UpdateSchoolDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public required string TelNumber { get; set; }
    public required SchoolLevel Level { get; set; }
    public required int ProvinceId { get; set; }
    public required int CityId { get; set; }
}
