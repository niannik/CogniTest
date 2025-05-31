using Application.Common;
using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.Users.Queries.GetAll;

public record GetAllUsersQuery : IRequest<Result<PaginatedList<GetAllUsersResponse>>>
{
    public PaginationDto Pagination { get; set; }
    public string? SearchTerm { get; set; }
    public bool? IsRightHanded { get; set; }
    public WorkingMemoryTestType? TestType { get; set; }
}
