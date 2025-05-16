using Api.Authorization;
using Api.Extensions;
using Application.Common.Interfaces;
using Application.SchoolPrincipals.Commands.SendOtp;
using Application.SchoolPrincipals.Commands.UpdateProfile;
using Application.SchoolPrincipals.Queries.GetProfile;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SchoolPrincipal)]

public class ProfilesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public ProfilesController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateSchoolPrincipalProfileDto dto)
    {
        var command = new UpdateSchoolPrincipalProfileCommand()
        {
            Id = _currentUserService.UserId!.Value,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            NationalCode = dto.NationalCode,
            SchoolId = dto.SchoolId
        };
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
    [HttpGet]
    public async Task<ActionResult<GetSchoolPrincipalProfileResponse>> GetProfile()
    {
        var query = new GetSchoolPrincipalProfileQuery(_currentUserService.UserId!.Value);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
