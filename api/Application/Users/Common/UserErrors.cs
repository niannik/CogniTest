using Application.Common;

namespace Application.Users.Common;

public class UserErrors
{
    public static Error PhoneNumberAlreadyExists = new("شماره تلفن قبلا ثبت شده است", "Phone_Number_Already_Exists");
}
