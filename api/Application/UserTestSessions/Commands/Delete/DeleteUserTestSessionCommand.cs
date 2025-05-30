using Application.Common;
using MediatR;

namespace Application.UserTestSessions.Commands.Delete;

public record DeleteUserTestSessionCommand(int TestId, int UserId) : IRequest<Result>;
