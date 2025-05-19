namespace Application.Schools.Queries.GetAllByCity;

public record GetAllSchoolsByCityResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
