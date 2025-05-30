using Application.Common;
using MediatR;

namespace Application.UserTestSessions.Queries.GetStatus;

public record GetUserTestSessionStatusQuery(int TestId, int UserId) : IRequest<Result<GetUserTestSessionStatusResponse>>;
