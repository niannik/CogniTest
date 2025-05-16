using Api.Authorization;
using Api.Extensions;
using Application.Cities.Commands.Create;
using Application.Provinces.Commands.Create;
using Application.Provinces.Queries.GetAll;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]
public class ProvincesController : ApiController
{
    private readonly IMediator _mediator;
    public ProvincesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllProvincesResponse>>> GetAll()
    {
        var query = new GetAllProvincesQuery();
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProvinceCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
