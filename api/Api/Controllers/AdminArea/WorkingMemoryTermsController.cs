using Api.Authorization;
using Api.Extensions;
using Application.WorkingMemoryTerms.Queries.GetStatistics;
using Application.WorkingMemoryTests.Commands.UploadAudio;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]

public class WorkingMemoryTermsController : ApiController
{
    private readonly IMediator _mediator;
    public WorkingMemoryTermsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<List<GetWorkingMemoryTermStatisticsResponse>>> GetStatistics([FromQuery] GetWorkingMemoryTermStatisticsQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
