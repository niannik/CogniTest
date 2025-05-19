using Api.Extensions;
using Application.Cities.Queries.GetAll;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
public class CitiesController : ApiController
{
    private readonly IMediator _mediator;
    public CitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllCitiesResponse>>> GetAll([FromQuery] GetAllCitiesQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
