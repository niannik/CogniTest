using Api.Authorization;
using Api.Extensions;
using Application.Admins.Commands.Login;
using Application.Admins.Commands.Logout;
using Application.Admins.Commands.RefreshAccessToken;
using Application.Admins.Common.Models;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]

public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AdminTokenDto>> Login([FromBody] AdminLoginCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost("refresh-token")]
    [Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]
    public async Task<ActionResult<AdminTokenDto>> RefreshToken([FromBody] RefreshAdminAccessTokenCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost("logout")]
    [Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]
    public async Task<ActionResult> Logout([FromBody] AdminLogoutCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}

