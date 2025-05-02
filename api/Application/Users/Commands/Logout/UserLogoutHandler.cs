using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands.Logout;

public class UserLogoutHandler : IRequestHandler<UserLogoutCommand, Result>
{
    private readonly ISignInUserService _signInUserService;
    public UserLogoutHandler(ISignInUserService signInUserService)
    {
        _signInUserService = signInUserService;
    }

    public async Task<Result> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
    {
        return await _signInUserService.LogoutAsync(request.refreshToken, cancellationToken);
    }
}
