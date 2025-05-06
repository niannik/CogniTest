using Application.Common;
using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.Schools.Queries.GetAll;

public record GetAllSchoolsQuery : IRequest<Result<PaginatedList<GetAllSchoolsResponse>>>
{
    public PaginationDto Pagination { get; set; }
    public string? SearchTerm { get; set; }
    public int? CityId { get; set; }
    public SchoolLevel? Level { get; set; }
}
