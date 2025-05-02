using Application.Common;

namespace Application.Users.Common;

public class UserErrors
{
    public static Error RefreshTokenIsNotValid = new("رفرش توکن معتبر نیست", "Refresh_Token_Is_Not_Valid");
}
