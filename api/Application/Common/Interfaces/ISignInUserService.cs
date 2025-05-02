using Application.Admins.Common.Models;
using Application.Users.Common.Models;
using Domain.Entities.AdminAggregate;
using Domain.Entities.UserAggregate;

namespace Application.Common.Interfaces;

public interface ISignInUserService
{
    public Task<UserTokenDto> LoginAsync(User user, CancellationToken cancellationToken = default);
    public Task<Result<UserTokenDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    public Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);
}
