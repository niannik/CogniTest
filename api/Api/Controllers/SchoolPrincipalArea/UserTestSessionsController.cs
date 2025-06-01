using Api.Authorization;
using Api.Extensions;
using Application.Common.Models;
using Application.UserTestSessions.Queries.GetAllBySchool;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SchoolPrincipal)]
public class UserTestSessionsController : ApiController
{
    private readonly IMediator _mediator;
    public UserTestSessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{schoolId:int}")]
    public async Task<ActionResult<PaginatedList<GetAllUserTestSessionsBySchoolResponse>>> GetAll([FromRoute] int schoolId, [FromQuery] GetAllUserTestSessionsBySchoolDto dto)
    {
        var query = new GetAllUserTestSessionsBySchoolQuery()
        {
            SchoolId = schoolId,
            TestType = dto.TestType,
            IsRightHanded = dto.IsRightHanded,
            SearchTerm = dto.SearchTerm,
            Pagination = dto.Pagination
        };
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
