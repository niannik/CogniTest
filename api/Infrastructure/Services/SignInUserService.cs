using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Common;
using Domain.Entities.UserAggregate;
using Application.Users.Common.Models;
using Microsoft.EntityFrameworkCore;
using Application.Users.Common;

namespace Infrastructure.Services;

public class SignInUserService : ISignInUserService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly TokenFactoryService _tokenFactoryService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISecurityService _securityService;
    private readonly BearerTokenSettings _bearerTokenSettings;

    public SignInUserService(IApplicationDbContext dbContext,
                             TokenFactoryService tokenFactoryService,
                             IDateTimeProvider dateTimeProvider,
                             ISecurityService securityService,
                             BearerTokenSettings bearerTokenSettings)
    {
        _tokenFactoryService = tokenFactoryService;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _securityService = securityService;
        _bearerTokenSettings = bearerTokenSettings;
    }

    public async Task<UserTokenDto> LoginAsync(User user, CancellationToken cancellationToken = default)
    {
        var jwtToken = _tokenFactoryService.CreateUserJwt(user);
        var refreshToken = _tokenFactoryService.CreateRefreshToken();

        var hashedRefreshToken = HashRefreshToken(refreshToken);
        var refreshTokenExpiresAt = _dateTimeProvider.Now.AddMinutes(_bearerTokenSettings.RefreshTokenExpirationMinutes);

        var userDevice = new UserDevice(hashedRefreshToken, refreshTokenExpiresAt) { User = user };
        _dbContext.UserDevices.Add(userDevice);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new UserTokenDto(jwtToken, refreshToken);
    }

    public async Task<Result<UserTokenDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var hashedRefreshToken = HashRefreshToken(refreshToken);

        var userDevice = await _dbContext.UserDevices
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.HashedRefreshToken == hashedRefreshToken, cancellationToken);

        if (userDevice == null || userDevice.RefreshTokenExpiresAt < _dateTimeProvider.Now)
            return UserErrors.RefreshTokenIsNotValid;

        var user = userDevice.User!;

        var newJwtToken = _tokenFactoryService.CreateUserJwt(user);
        var newRefreshToken = _tokenFactoryService.CreateRefreshToken();
        userDevice.HashedRefreshToken = HashRefreshToken(newRefreshToken);
        userDevice.RefreshTokenExpiresAt = _dateTimeProvider.Now.AddMinutes(_bearerTokenSettings.AdminRefreshTokenExpirationMinutes);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new UserTokenDto(newJwtToken, newRefreshToken);
    }

    public async Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserDevices
            .Where(x => x.HashedRefreshToken == HashRefreshToken(refreshToken))
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }

    private string HashRefreshToken(string input) =>
        _securityService.GetSha256Hash(input);
}
