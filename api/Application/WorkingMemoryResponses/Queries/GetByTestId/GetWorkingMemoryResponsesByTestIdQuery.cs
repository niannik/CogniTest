using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetByTestId;

public record GetWorkingMemoryResponsesByTestIdQuery(int TestId, int UserId) : IRequest<Result<GetWorkingMemoryResponsesByTestIdResponse>>;