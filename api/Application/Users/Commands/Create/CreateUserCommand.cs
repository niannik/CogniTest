using Application.Common;
using Application.Users.Common.Models;
using Domain.Enums;
using MediatR;

namespace Infrastructure;

public class CreateUserCommand : IRequest<Result<UserTokenDto>>
{
    public required string PhoneNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
    public required Gender Gender { get; set; }
    public required bool IsRightHanded { get; set; }
    public required int SchoolId { get; set; }
}
