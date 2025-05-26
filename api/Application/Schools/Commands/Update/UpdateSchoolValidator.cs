using FluentValidation;

namespace Application.Schools.Commands.Update;

public class UpdateSchoolValidator : AbstractValidator<UpdateSchoolCommand>
{
    public UpdateSchoolValidator()
    {
        RuleFor(x => x.PostalCode)
            .Length(10)
            .WithMessage("کد پستی باید 10 رقمی باشد");
    }
}
