using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetByTest;

public record GetWorkingMemoryResponsesByTestQuery(int TestId, int UserId) : IRequest<Result<GetWorkingMemoryResponsesByTestResponse>>;