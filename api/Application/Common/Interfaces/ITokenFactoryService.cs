using Domain.Entities.UserAggregate;

namespace Application.Common.Interfaces;

public interface ITokenFactoryService
{
    public string CreateUserJwt(User user);
    public string CreateRefreshToken();
}