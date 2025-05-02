using Application.Admins.Common.Models;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Admins.Commands.RefreshAccessToken;

public class RefreshAdminAccessTokenHandler : IRequestHandler<RefreshAdminAccessTokenCommand, Result<AdminTokenDto>>
{
    private readonly ISignInAdminService _signInAdminService;
    public RefreshAdminAccessTokenHandler(ISignInAdminService signInAdminService)
    {
        _signInAdminService = signInAdminService;
    }
    public Task<Result<AdminTokenDto>> Handle(RefreshAdminAccessTokenCommand request, CancellationToken cancellationToken)
    {
        return _signInAdminService.RefreshAccessTokenAsync(request.refreshToken, cancellationToken);
    }
}
