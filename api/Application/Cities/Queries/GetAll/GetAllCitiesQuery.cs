using Application.Common;
using MediatR;

namespace Application.Cities.Queries.GetAll;

public record GetAllCitiesQuery() : IRequest<Result<List<GetAllCitiesResponse>>>;
