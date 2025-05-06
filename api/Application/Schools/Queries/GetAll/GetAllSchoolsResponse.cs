using Domain.Enums;

namespace Application.Schools.Queries.GetAll;

public record GetAllSchoolsResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public required string TelNumber { get; set; }
    public required SchoolLevel Level { get; set; }
    public required string ProvinceName { get; set; }
    public required string CityName { get; set; }
}
