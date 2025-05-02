using Domain.Common;
using Domain.Entities.SchoolAggregate;

namespace Domain.Entities.CityAggregate;

public class City : Entity
{
    public City(string name, int provinceId)
    {
        Name = name;
        ProvinceId = provinceId;
    }

    public string Name { get; set; }
    public int ProvinceId { get; set; }

    public Province? Province { get; set; }
    public List<School>? Schools { get; set; }
}
