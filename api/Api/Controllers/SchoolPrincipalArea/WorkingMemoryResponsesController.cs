using Api.Extensions;
using Application.Common.Interfaces;
using Application.WorkingMemoryResponses.Queries.GetBySchool;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
[Authorize]
public class WorkingMemoryResponsesController : ApiController
{
    private readonly IMediator _mediator;
    public WorkingMemoryResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GetWorkingMemoryResponsesBySchoolVm>>> GetByTestId([FromQuery] GetWorkingMemoryResponsesBySchoolQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
