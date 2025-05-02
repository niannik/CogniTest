using Application.Common;

namespace Application.Admins.Common;

public class AdminErrors
{
    public static Error UserNameIsWrong = new("نام کاربری نادرست است", "User_Name_Is_Wrong");
    public static Error PasswordIsWrong = new("رمز عبور نادرست است", "Password_Is_Wrong");
    public static Error RefreshTokenIsNotValid = new("رفرش توکن معتبر نیست", "Refresh_Token_Is_Not_Valid");
}
