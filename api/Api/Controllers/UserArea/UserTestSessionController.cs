using Api.Extensions;
using Application.Common.Interfaces;
using Application.UserTestSessions.Commands.Create;
using Application.UserTestSessions.Commands.Delete;
using Application.UserTestSessions.Queries.GetStatus;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[Controller]")]
[Authorize]

public class UserTestSessionController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public UserTestSessionController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpPost("{testId:int}")]
    public async Task<ActionResult> Create([FromRoute] int testId)
    {
        var command = new CreateUserTestSessionCommand(testId, _currentUserService.UserId!.Value);
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }


    [HttpDelete("{testId:int}")]
    public async Task<ActionResult> Delete([FromRoute] int testId)
    {
        var command = new DeleteUserTestSessionCommand(testId, _currentUserService.UserId!.Value);
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }


    [HttpGet("{testId:int}")]
    public async Task<ActionResult<GetUserTestSessionStatusResponse>> GetStatus([FromRoute] int testId)
    {
        var command = new GetUserTestSessionStatusQuery(testId, _currentUserService.UserId!.Value);
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
