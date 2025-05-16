using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.SchoolPrincipals.Common.Models;
using Application.Users.Common.Models;
using Domain.Entities.SchoolAggregate;
using Domain.Entities.UserAggregate;

namespace Infrastructure.Services;

public class SignInSchoolPrincipalService : ISignInSchoolPrincipalService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly TokenFactoryService _tokenFactoryService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISecurityService _securityService;
    private readonly BearerTokenSettings _bearerTokenSettings;

    public SignInSchoolPrincipalService(IApplicationDbContext dbContext,
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

    public async Task<SchoolPrincipalTokenDto> LoginAsync(SchoolPrincipal schoolPrincipal, CancellationToken cancellationToken = default)
    {
        var jwtToken = _tokenFactoryService.CreateSchoolPrincipalJwt(schoolPrincipal);
        var refreshToken = _tokenFactoryService.CreateRefreshToken();

        var hashedRefreshToken = HashRefreshToken(refreshToken);
        var refreshTokenExpiresAt = _dateTimeProvider.Now.AddMinutes(_bearerTokenSettings.RefreshTokenExpirationMinutes);

        var userDevice = new UserDevice(hashedRefreshToken, refreshTokenExpiresAt) { SchoolPrincipal = schoolPrincipal };
        _dbContext.UserDevices.Add(userDevice);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new SchoolPrincipalTokenDto(jwtToken, refreshToken);
    }

    private string HashRefreshToken(string input) =>
        _securityService.GetSha256Hash(input);
}
