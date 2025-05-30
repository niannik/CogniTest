using Application.Common;
using MediatR;

namespace Application.UserTestSessions.Commands.Create;

public record CreateUserTestSessionCommand(int TestId, int UserId) : IRequest<Result>;
