using Application.Admins.Common.Models;
using Application.Common;
using MediatR;

namespace Application.Admins.Commands.Login;

public record AdminLoginCommand : IRequest<Result<AdminTokenDto>>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
