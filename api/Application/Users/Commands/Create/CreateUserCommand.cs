using Application.Common;
using Application.Users.Common.Models;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure;

public record CreateUserCommand : IRequest<Result<UserTokenDto>>
{
    [RegularExpression(@"^09[0|1|2|3|9][0-9]{8}$", ErrorMessage = "شماره موبایل وارد شده معتبر نیست.")]
    public required string PhoneNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
    public required Gender Gender { get; set; }
    public required bool IsRightHanded { get; set; }
    public required int SchoolId { get; set; }
}
