using Api.Extensions;
using Application.Common.Interfaces;
using Application.WorkingMemoryResponses.Command.Create;
using Application.WorkingMemoryResponses.Queries.GetByTest;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[Controller]")]
[Authorize]
public class WorkingMemoryResponsesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public WorkingMemoryResponsesController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpPost("{testId:int}")]
    public async Task<ActionResult> Create([FromRoute] int testId, [FromBody] CreateWorkingMemoryResponseDto dto)
    {
        var command = new CreateWorkingMemoryResponseCommand()
        {
            TestId = testId,
            UserId = _currentUserService.UserId!.Value,
            TermId = dto.TermId,
            ResponseTime = dto.ResponseTime,
            IsTarget = dto.IsTarget
        };
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpGet("{testId:int}")]
    public async Task<ActionResult<GetWorkingMemoryResponsesByTestResponse>> GetByTestId([FromRoute] int testId)
    {
        var query = new GetWorkingMemoryResponsesByTestQuery(testId, _currentUserService.UserId!.Value);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
