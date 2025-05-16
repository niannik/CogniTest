using Application.Common;
using MediatR;

namespace Application.SchoolPrincipals.Queries.GetProfile;

public record GetSchoolPrincipalProfileQuery(int Id) : IRequest<Result<GetSchoolPrincipalProfileResponse>>;
