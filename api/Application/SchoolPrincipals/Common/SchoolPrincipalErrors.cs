using Application.Common;

namespace Application.SchoolPrincipals.Common;

public class SchoolPrincipalErrors
{
    public static Error SchoolPrincipalNotFound = new("مدیر مدرسه یافت نشد", "School_Principal_Not_Found");
    public static Error TooManyAttempts = new("تعداد تلاش ها بیش از حد", "Too_Many_Attempts");
    public static Error OtpCodeIsNotValid = new("کد یکبار مصرف نادرست است", "Otp_Code_Is_Not_Valid");
    public static Error OtpCodeIsExpired = new("کد یکبار مصرف منقضی شده است", "Otp_Code_Is_Expired");
}
