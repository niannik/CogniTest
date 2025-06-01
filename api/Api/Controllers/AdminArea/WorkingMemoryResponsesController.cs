using Api.Authorization;
using Api.Extensions;
using Application.WorkingMemoryResponses.Queries.GetAccuracy;
using Application.WorkingMemoryResponses.Queries.GetBySession;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]

public class WorkingMemoryResponsesController : ApiController
{
    private readonly IMediator _mediator;
    public WorkingMemoryResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{sessionId:int}")]
    public async Task<ActionResult<List<GetWorkingMemoryResponsesByTestSessionResponse>>> GetBySession([FromRoute] int sessionId)
    {
        var query = new GetWorkingMemoryResponsesByTestSessionQuery(sessionId);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
    
    [HttpGet("{sessionId:int}/accuracy-chart")]
    public async Task<ActionResult<GetWorkingMemoryResponseAccuracyResponse>> GetAccuracy([FromRoute] int sessionId)
    {
        var query = new GetWorkingMemoryResponseAccuracyQuery(sessionId);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
