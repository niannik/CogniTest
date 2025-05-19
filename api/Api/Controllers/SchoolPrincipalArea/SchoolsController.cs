using Api.Extensions;
using Application.Common.Models;
using Application.Schools.Queries.GetAll;
using Application.Schools.Queries.GetAllByCity;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SchoolPrincipalArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/school-principal/[Controller]")]
public class SchoolsController : ApiController
{
    private readonly IMediator _mediator;
    public SchoolsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllSchoolsByCityResponse>>> GetAll([FromQuery] GetAllSchoolsByCityQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
