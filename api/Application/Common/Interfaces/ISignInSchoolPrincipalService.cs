using Application.SchoolPrincipals.Common.Models;
using Domain.Entities.SchoolAggregate;

namespace Application.Common.Interfaces;

public interface ISignInSchoolPrincipalService
{
    public Task<SchoolPrincipalTokenDto> LoginAsync(SchoolPrincipal schoolPrincipal, CancellationToken cancellationToken = default);
}
