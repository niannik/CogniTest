using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Admins.Commands.Logout;

public class AdminLogoutHandler : IRequestHandler<AdminLogoutCommand, Result>
{
    private readonly ISignInAdminService _signInAdminService;
    public AdminLogoutHandler(ISignInAdminService signInAdminService)
    {
        _signInAdminService = signInAdminService;
    }

    public Task<Result> Handle(AdminLogoutCommand request, CancellationToken cancellationToken)
    {
        return _signInAdminService.LogoutAsync(request.refreshToken, cancellationToken);
    }
}
