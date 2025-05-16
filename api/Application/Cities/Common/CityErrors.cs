using Application.Common;

namespace Application.Cities.Common;

public class CityErrors
{
    public static Error CityNotFound = new("شهر یافت نشد", "City_Not_Found");
    public static Error CityAlreadyExists = new("شهر درحال حاضر وجود دارد", "City_Already_Exists");
}
