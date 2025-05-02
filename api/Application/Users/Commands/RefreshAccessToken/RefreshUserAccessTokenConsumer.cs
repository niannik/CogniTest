using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Common.Models;
using MediatR;

namespace Application.Users.Commands.RefreshAccessToken;

public class RefreshUserAccessTokenConsumer : IRequestHandler<RefreshUserAccessTokenCommand, Result<UserTokenDto>>
{
    private readonly ISignInUserService _signInUserService;
    public RefreshUserAccessTokenConsumer(ISignInUserService signInUserService)
    {
        _signInUserService = signInUserService;
    }

    public async Task<Result<UserTokenDto>> Handle(RefreshUserAccessTokenCommand request, CancellationToken cancellationToken)
    {
        return await _signInUserService.RefreshAccessTokenAsync(request.refreshToken, cancellationToken);
    }
}
