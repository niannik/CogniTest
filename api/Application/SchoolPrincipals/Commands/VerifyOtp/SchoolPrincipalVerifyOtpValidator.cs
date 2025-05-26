using FluentValidation;

namespace Application.SchoolPrincipals.Commands.VerifyOtp;

public class SchoolPrincipalVerifyOtpValidator : AbstractValidator<SchoolPrincipalVerifyOtpCommand>
{
    public SchoolPrincipalVerifyOtpValidator()
    {
        RuleFor(x => x.OtpCode)
            .Length(5)
            .WithMessage("کد یکبار مصرف باید 5 رقمی باشد");
    }
}
