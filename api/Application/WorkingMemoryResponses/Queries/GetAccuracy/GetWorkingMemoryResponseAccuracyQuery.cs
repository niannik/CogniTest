using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetAccuracy;

public record GetWorkingMemoryResponseAccuracyQuery(int UserTestSessionId) : IRequest<Result<GetWorkingMemoryResponseAccuracyResponse>>;