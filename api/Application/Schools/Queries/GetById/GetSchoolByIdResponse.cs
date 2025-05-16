using Domain.Enums;

namespace Application.Schools.Queries.GetById;

public record GetSchoolByIdResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public required string TelNumber { get; set; }
    public required SchoolLevel Level { get; set; }
    public required string ProvinceName { get; set; }
    public required string CityName { get; set; }
    public required SchoolPrincipalDetail PrincipalDetail { get; set; }
}
public record SchoolPrincipalDetail
{
    public int? Id { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NationalCode { get; set; }
}
