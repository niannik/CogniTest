using Domain.Common;
using Domain.Entities.CityAggregate;
using Domain.Entities.UserAggregate;
using Domain.Enums;

namespace Domain.Entities.SchoolAggregate;

public class School : AuditableEntity
{
    public School(string name, string address, string postalCode, string telNumber, SchoolLevel level, int cityId)
    {
        Name = name;
        Address = address;
        TelNumber = telNumber;
        Level = level;
        CityId = cityId;
        PostalCode = postalCode;
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string TelNumber { get; set; }
    public SchoolLevel Level { get; set; }
    public int CityId { get; set; }

    public City? City { get; set; }
    public SchoolPrincipal? Principal { get; set; }
    public List<User>? Students { get; set; }
}
