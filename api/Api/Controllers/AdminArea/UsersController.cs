using Api.Authorization;
using Api.Extensions;
using Application.Common.Models;
using Application.Schools.Queries.GetAll;
using Application.Users.Queries.GetAll;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]
public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<GetAllUsersResponse>>> GetAll([FromQuery] GetAllUsersQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
