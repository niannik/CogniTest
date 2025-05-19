using Application.Common;
using MediatR;

namespace Application.Schools.Queries.GetAllByCity;

public record GetAllSchoolsByCityQuery(int? CityId, string? SearchTerm) : IRequest<Result<List<GetAllSchoolsByCityResponse>>>;
