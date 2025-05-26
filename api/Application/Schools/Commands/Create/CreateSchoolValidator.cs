using FluentValidation;

namespace Application.Schools.Commands.Create;

public class CreateSchoolValidator : AbstractValidator<CreateSchoolCommand>
{
    public CreateSchoolValidator()
    {
        RuleFor(x => x.PostalCode)
            .Length(10)
            .WithMessage("کد پستی باید 10 رقمی باشد");
    }
}
