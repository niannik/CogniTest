using Application.Common;

namespace Application.Schools.Common;

public class SchoolErrors
{
    public static Error SchoolNotFound = new Error("مدرسه یافت نشد", "School_Not_Found");
    public static Error CannotRemoveSchoolThatHasStudents = new Error("امکان پاک کردن مدرسه ای که دانش آموز دارد وجود ندارد", "Cannot_Remove_School_That_Has_Students");
}
