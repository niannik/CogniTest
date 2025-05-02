using Application.Common.Interfaces;
using Application.Common.Settings;
using Domain.Entities.AdminAggregate;
using Domain.Entities.SchoolAggregate;
using Domain.Entities.UserAggregate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenFactoryService : ITokenFactoryService
{
    private readonly ISecurityService _securityService;
    private readonly BearerTokenSettings _bearerTokenSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenFactoryService(ISecurityService securityService, BearerTokenSettings bearerTokenSettings, IDateTimeProvider dateTimeProvider)
    {
        _securityService = securityService;
        _bearerTokenSettings = bearerTokenSettings;
        _dateTimeProvider = dateTimeProvider;
    }


    public string CreateRefreshToken()
        => _securityService.CreateRandomString();

    public string CreateUserJwt(User user)
    {
        var claims = CreateUserAccessTokenClaims(user);

        return CreateAccessToken(claims);
    }
    public string CreateSellerJwt(SchoolPrincipal schoolPrincipal)
    {
        var claims = CreateSchoolPrincipalAccessTokenClaims(schoolPrincipal);

        return CreateAccessToken(claims);
    }

    public string CreateAdminJwt(Admin admin)
    {
        var claims = CreateAdminAccessTokenClaims(admin);

        return CreateAccessToken(claims);
    }

    private static List<Claim> CreateUserAccessTokenClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.MobilePhone, user.PhoneNumber)
        };

        return claims;
    }
    private static List<Claim> CreateSchoolPrincipalAccessTokenClaims(SchoolPrincipal schoolPrincipal)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.NameIdentifier, schoolPrincipal.Id.ToString()),
            new(ClaimTypes.MobilePhone, schoolPrincipal.PhoneNumber),
            new(ClaimTypes.Role, "SchoolPrincipal")
        };

        return claims;
    }

    private static List<Claim> CreateAdminAccessTokenClaims(Admin admin)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new(ClaimTypes.Email, admin.UserName),
            new(ClaimTypes.Role, "SuperAdmin")
        };

        return claims;
    }

    private static readonly JwtSecurityTokenHandler JwtHandler = new();

    private string CreateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokenSettings.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var now = _dateTimeProvider.Now;

        var token = new JwtSecurityToken(
                     issuer: _bearerTokenSettings.Issuer,
                     audience: _bearerTokenSettings.Audience,
                     claims: claims,
                     notBefore: now,
                     expires: now.AddMinutes(_bearerTokenSettings.AccessTokenExpirationMinutes),
                     signingCredentials: signingCredentials);

        return JwtHandler.WriteToken(token);
    }
}
