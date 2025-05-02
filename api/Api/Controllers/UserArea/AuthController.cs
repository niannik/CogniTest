using Api.Authorization;
using Api.Extensions;
using Application.Admins.Commands.Logout;
using Application.Admins.Commands.RefreshAccessToken;
using Application.Admins.Common.Models;
using Application.Users.Commands.Logout;
using Application.Users.Commands.RefreshAccessToken;
using Application.Users.Common.Models;
using Asp.Versioning;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[Controller]")]

public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<UserTokenDto>> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost("refresh-token")]
    [Authorize]
    public async Task<ActionResult<UserTokenDto>> RefreshToken([FromBody] RefreshUserAccessTokenCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult> Logout([FromBody] UserLogoutCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
