using Application.Admins.Common.Models;
using Application.Common;
using MediatR;

namespace Application.Admins.Commands.RefreshAccessToken;

public record RefreshAdminAccessTokenCommand(string refreshToken) : IRequest<Result<AdminTokenDto>>;
