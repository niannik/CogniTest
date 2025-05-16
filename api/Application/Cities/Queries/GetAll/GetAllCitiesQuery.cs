using Application.Common;
using MediatR;

namespace Application.Cities.Queries.GetAll;

public record GetAllCitiesQuery(string? SearchTerm) : IRequest<Result<List<GetAllCitiesResponse>>>;
