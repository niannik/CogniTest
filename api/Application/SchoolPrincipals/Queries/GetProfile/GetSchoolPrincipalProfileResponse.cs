namespace Application.SchoolPrincipals.Queries.GetProfile;

public record GetSchoolPrincipalProfileResponse
{
    public required int Id { get; set; }
    public required string PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public SchoolPrincipalSchoolDetail? SchoolDetail { get; set; }
}
public record SchoolPrincipalSchoolDetail
{
    public int? Id { get; set; }
    public string? Name { get; set; } 
}