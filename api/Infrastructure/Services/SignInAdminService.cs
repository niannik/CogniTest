using Application.Admins.Common.Models;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Common;
using Domain.Entities.AdminAggregate;
using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Application.Admins.Common;

namespace Infrastructure.Services;

public class SignInAdminService : ISignInAdminService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly TokenFactoryService _tokenFactoryService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISecurityService _securityService;
    private readonly BearerTokenSettings _bearerTokenSettings;

    public SignInAdminService(IApplicationDbContext dbContext, 
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

    public async Task<AdminTokenDto> LoginAsync(Admin admin,CancellationToken cancellationToken = default)
    {
        var jwtToken = _tokenFactoryService.CreateAdminJwt(admin);
        var refreshToken = _tokenFactoryService.CreateRefreshToken();

        var hashedRefreshToken = HashRefreshToken(refreshToken);
        var refreshTokenExpiresAt = _dateTimeProvider.Now.AddMinutes(_bearerTokenSettings.AdminRefreshTokenExpirationMinutes);

        var adminDevice = new UserDevice(hashedRefreshToken, refreshTokenExpiresAt) { Admin = admin };
        _dbContext.UserDevices.Add(adminDevice);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new AdminTokenDto(jwtToken, refreshToken);
    }

    public async Task<Result<AdminTokenDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var hashedRefreshToken = HashRefreshToken(refreshToken);

        var userDevice = await _dbContext.UserDevices
            .Include(x => x.Admin)
            .FirstOrDefaultAsync(x => x.HashedRefreshToken == hashedRefreshToken, cancellationToken);

        if (userDevice == null || userDevice.RefreshTokenExpiresAt < _dateTimeProvider.Now)
            return AdminErrors.RefreshTokenIsNotValid;

        var admin = userDevice.Admin!;

        var newJwtToken = _tokenFactoryService.CreateAdminJwt(admin);
        var newRefreshToken = _tokenFactoryService.CreateRefreshToken();
        userDevice.HashedRefreshToken = HashRefreshToken(newRefreshToken);
        userDevice.RefreshTokenExpiresAt = _dateTimeProvider.Now.AddMinutes(_bearerTokenSettings.AdminRefreshTokenExpirationMinutes);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new AdminTokenDto(newJwtToken, newRefreshToken);
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
