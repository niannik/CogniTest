using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.WorkingMemoryTests.Queries.GatAll;

public record GetAllWorkingMemoryTestsQuery(int UserId) : IRequest<Result<List<GetAllWorkingMemoryTestsResponse>>>;
