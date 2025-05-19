using Api.Authorization;
using Api.Extensions;
using Application.Cities.Commands.Create;
using Application.Provinces.Commands.Create;
using Application.Provinces.Queries.GetAll;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[Controller]")]
public class ProvincesController : ApiController
{
    private readonly IMediator _mediator;
    public ProvincesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllProvincesResponse>>> GetAll([FromQuery] GetAllProvincesQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
