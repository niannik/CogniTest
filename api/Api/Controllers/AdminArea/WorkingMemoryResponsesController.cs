using Api.Authorization;
using Api.Extensions;
using Application.WorkingMemoryResponses.Queries.GetBySession;
using Application.WorkingMemoryTests.Commands.UploadAudio;
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

    [HttpGet]
    public async Task<ActionResult<List<GetWorkingMemoryResponsesByTestSessionResponse>>> GetBySession([FromQuery] GetWorkingMemoryResponsesByTestSessionQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
