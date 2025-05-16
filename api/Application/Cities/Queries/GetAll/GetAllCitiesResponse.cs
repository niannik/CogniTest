namespace Application.Cities.Queries.GetAll;

public record GetAllCitiesResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required CityProvinceDetail ProvinceDetail { get; set; }
}

public record CityProvinceDetail
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
