using Application.Common;
using MediatR;

namespace Application.Provinces.Queries.GetAll;

public record GetAllProvincesQuery() : IRequest<Result<List<GetAllProvincesResponse>>>;