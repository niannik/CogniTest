using Api.Extensions;
using Application.SchoolPrincipals.Commands.SendOtp;
using Application.SchoolPrincipals.Commands.VerifyOtp;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("send-otp")]
    public async Task<ActionResult<SchoolPrincipalSendOtpResponse>> SendOtp([FromBody] SchoolPrincipalSendOtpCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
    
    [HttpPost("verify-otp")]
    public async Task<ActionResult<SchoolPrincipalVerifyOtpResponse>> VerifyOtp([FromBody] SchoolPrincipalVerifyOtpCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
