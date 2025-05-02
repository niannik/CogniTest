using Domain.Common;
using Domain.Entities.AdminAggregate;
using Domain.Entities.SchoolAggregate;

namespace Domain.Entities.UserAggregate;

public class UserDevice : Entity
{
    public UserDevice(string hashedRefreshToken, DateTime refreshTokenExpiresAt)
    {
        HashedRefreshToken = hashedRefreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }

    public string HashedRefreshToken { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
    public int? UserId { get; set; }
    public int? AdminId { get; set; }
    public int? SchoolPrincipalId { get; set; }

    public User? User { get; set; }
    public Admin? Admin { get; set; }
    public SchoolPrincipal? SchoolPrincipal { get; set; }

}
