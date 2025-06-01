namespace Application.Schools.Queries.GetAllBySchoolPrincipal;

public record GetAllBySchoolPrincipalResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
