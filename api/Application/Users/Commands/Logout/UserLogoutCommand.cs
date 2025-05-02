using Application.Common;
using MediatR;

namespace Application.Users.Commands.Logout;

public record UserLogoutCommand (string refreshToken) : IRequest<Result>;
