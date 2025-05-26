using FluentValidation;

namespace Application.SchoolPrincipals.Commands.UpdateProfile;

public class UpdateSchoolPrincipalProfileValidator : AbstractValidator<UpdateSchoolPrincipalProfileCommand>
{
    public UpdateSchoolPrincipalProfileValidator()
    {
        RuleFor(x => x.NationalCode)
            .Length(10)
            .WithMessage("کد ملی معتبر نیست");
    }
}
