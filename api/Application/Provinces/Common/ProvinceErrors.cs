using Application.Common;

namespace Application.Provinces.Common;

public class ProvinceErrors
{
    public static Error ProvinceNotFound = new("استان یافت نشد", "Province_Not_Found");
    public static Error ProvinceAlreadyExists = new("استان درحال حاضر وجود دارد", "Province_Already_Exists");
}
