using Application.Common;

namespace Application.WorkingMemoryTerms.Common;

public class WorkingMemoryTermErrors
{
    public static Error WorkingMemoryTermNotFound = new Error("سوال آزمون حافظه فعال یافت نشد", "Working_Memory_Term_Not_Found");
    public static Error WorkingMemoryTermAlreadyAnswered = new Error("سوال آزمون حافظه فعال قبلا پاسخ داده شده است", "Working_Memory_Term_Already_Answered");
}
