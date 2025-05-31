using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetBySession;

public record GetWorkingMemoryResponsesByTestSessionQuery(int UserTestSessionId) : IRequest<Result<List<GetWorkingMemoryResponsesByTestSessionResponse>>>;
