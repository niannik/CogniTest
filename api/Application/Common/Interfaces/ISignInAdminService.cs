using Application.Admins.Common.Models;
using Domain.Entities.AdminAggregate;

namespace Application.Common.Interfaces;

public interface ISignInAdminService
{
    public Task<AdminTokenDto> LoginAsync(Admin admin, CancellationToken cancellationToken = default);
    public Task<Result<AdminTokenDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    public Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);
}
