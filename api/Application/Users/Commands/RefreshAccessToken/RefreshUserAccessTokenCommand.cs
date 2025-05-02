using Application.Common;
using Application.Users.Common.Models;
using MediatR;

namespace Application.Users.Commands.RefreshAccessToken;

public record RefreshUserAccessTokenCommand(string refreshToken) : IRequest<Result<UserTokenDto>>;
