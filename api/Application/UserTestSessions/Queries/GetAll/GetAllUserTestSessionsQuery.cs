using Application.Common;
using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.UserTestSessions.Queries.GetAll;

public record GetAllUserTestSessionsQuery : IRequest<Result<PaginatedList<GetAllUserTestSessionsResponse>>>
{
    public PaginationDto Pagination { get; set; }
    public string? SearchTerm { get; set; }
    public bool? IsRightHanded { get; set; }
    public WorkingMemoryTestType? TestType { get; set; }
}
