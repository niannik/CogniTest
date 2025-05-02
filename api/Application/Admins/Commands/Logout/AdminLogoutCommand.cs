using Application.Common;
using MediatR;

namespace Application.Admins.Commands.Logout;

public record AdminLogoutCommand(string refreshToken) : IRequest<Result>;
