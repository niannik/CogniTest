using Application.Common;
using MediatR;

namespace Application.Provinces.Queries.GetAll;

public record GetAllProvincesQuery(string? SearchTerm) : IRequest<Result<List<GetAllProvincesResponse>>>;