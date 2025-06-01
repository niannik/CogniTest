using Api.Authorization;
using Api.Extensions;
using Application.WorkingMemoryResponses.Queries.GetAccuracy;
using Application.WorkingMemoryResponses.Queries.GetAccuracyBySchool;
using Application.WorkingMemoryResponses.Queries.GetBySchool;
using Application.WorkingMemoryResponses.Queries.GetBySession;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SchoolPrincipal)]
public class WorkingMemoryResponsesController : ApiController
{
    private readonly IMediator _mediator;
    public WorkingMemoryResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{sessionId:int}")]
    public async Task<ActionResult<List<GetWorkingMemoryResponsesBySchoolResponse>>> GetBySession([FromRoute] int sessionId, [FromQuery] int schoolId)
    {
        var query = new GetWorkingMemoryResponsesBySchoolQuery(sessionId, schoolId);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpGet("{sessionId:int}/accuracy-chart")]
    public async Task<ActionResult<GetWorkingMemoryResponseAccuracyBySchoolResponse>> GetAccuracy([FromRoute] int sessionId, [FromQuery] int schoolId)
    {
        var query = new GetWorkingMemoryResponseAccuracyBySchoolQuery(sessionId, schoolId);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
    
}
