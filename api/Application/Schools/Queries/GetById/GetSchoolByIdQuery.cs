using Application.Common;
using MediatR;

namespace Application.Schools.Queries.GetById;

public record GetSchoolByIdQuery(int Id) : IRequest<Result<GetSchoolByIdResponse>>;
