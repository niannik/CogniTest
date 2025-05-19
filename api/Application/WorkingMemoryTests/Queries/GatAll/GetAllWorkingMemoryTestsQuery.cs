using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.WorkingMemoryTests.Queries.GatAll;

public record GetAllWorkingMemoryTestsQuery(int Id) : IRequest<Result<List<GetAllWorkingMemoryTestsResponse>>>;
