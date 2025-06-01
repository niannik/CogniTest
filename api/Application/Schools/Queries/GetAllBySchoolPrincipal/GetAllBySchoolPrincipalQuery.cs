using Application.Common;
using MediatR;

namespace Application.Schools.Queries.GetAllBySchoolPrincipal;

public record GetAllBySchoolPrincipalQuery(int? CityId, string? SearchTerm) : IRequest<Result<List<GetAllBySchoolPrincipalResponse>>>;
