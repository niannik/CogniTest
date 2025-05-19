using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Command.Delete;

public record DeleteWorkingMemoryResponseCommand(int TestId, int UserId) : IRequest<Result>;
