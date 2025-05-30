using Application.Common;

namespace Application.UserTestSessions.Common;

public class UserTestSessionErrors
{
    public static Error UserTestSessionNotFound = new("آزمون یافت نشد", "User_Test_Session_Not_Found");
    public static Error UserTestSessionCompleted = new("آزمون تکمیل شده است", "User_Test_Session_Completed");
    public static Error UserTestSessionAlreadyExists = new("آزمون وجود دارد", "User_Test_Session_Already_Exists");
    public static Error UserAlreadyHasActiveTestSession = new("کاربر درحال حاضر یک آزمون فعال دارد", "User_Already_Has_Active_Test_Session");
    public static Error TestResultUnavailableForActiveSession = new("نتیجه آزمون فعال در دسترس نمیباشد", "Test_Result_Unavailable_For_Active_Session");
    public static Error CannotCancelCompletedTestSession = new("امکان انصراف از آزمون تکمیل شده وجود ندارد", "Cannot_Cancel_Completed_Test_Session");
}
