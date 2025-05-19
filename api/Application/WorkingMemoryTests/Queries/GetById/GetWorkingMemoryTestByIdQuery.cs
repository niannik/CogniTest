using Application.Common;
using MediatR;

namespace Application.WorkingMemoryTests.Queries.GetById;

public record GetWorkingMemoryTestByIdQuery(int Id) : IRequest<Result<GetWorkingMemoryTestByIdResponse>>;
