using Domain.Common;

namespace Domain.Entities.CityAggregate;

public class Province : Entity
{
    public string Name { get; set; }

    public List<City>? Cities { get; set; }
}
