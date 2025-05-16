namespace Application.SchoolPrincipals.Commands.UpdateProfile;

public record UpdateSchoolPrincipalProfileDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NationalCode { get; set; }
    public required int SchoolId { get; set; }
}
