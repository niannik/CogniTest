using Application.Common.Models;
using Domain.Enums;

namespace Application.UserTestSessions.Queries.GetAllBySchool;

public record GetAllUserTestSessionsBySchoolDto
{
    public PaginationDto Pagination { get; set; }
    public string? SearchTerm { get; set; }
    public bool? IsRightHanded { get; set; }
    public required WorkingMemoryTestType TestType { get; set; }
}
