namespace Application.Provinces.Queries.GetAll;

public record GetAllProvincesResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
