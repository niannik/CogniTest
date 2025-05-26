using FluentValidation;
using Infrastructure;

namespace Application.Users.Commands.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(5)
            .WithMessage("حداقل سن مجاز برای شرکت در آزمون 5 سال است")
            .LessThanOrEqualTo(20)
            .WithMessage("حداکثر سن مجاز برای شرکت در آزمون 20 سال است");
    }
}
