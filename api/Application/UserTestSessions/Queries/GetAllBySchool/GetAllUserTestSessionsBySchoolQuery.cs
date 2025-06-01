using Application.Common.Models;
using Application.Common;
using Application.UserTestSessions.Queries.GetAll;
using MediatR;
using Domain.Enums;

namespace Application.UserTestSessions.Queries.GetAllBySchool;

public record GetAllUserTestSessionsBySchoolQuery : IRequest<Result<PaginatedList<GetAllUserTestSessionsBySchoolResponse>>>
{
    public PaginationDto Pagination { get; set; }
    public string? SearchTerm { get; set; }
    public bool? IsRightHanded { get; set; }
    public required WorkingMemoryTestType TestType { get; set; }
    public required int SchoolId { get; set; }
}
