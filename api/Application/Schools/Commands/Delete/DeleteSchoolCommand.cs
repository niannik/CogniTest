using Application.Common;
using MediatR;

namespace Application.Schools.Commands.Delete;

public record DeleteSchoolCommand(int Id) : IRequest<Result>;