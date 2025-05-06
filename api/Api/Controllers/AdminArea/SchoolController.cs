using Api.Authorization;
using Api.Extensions;
using Application.Common.Models;
using Application.Schools.Commands.Create;
using Application.Schools.Commands.Delete;
using Application.Schools.Commands.Update;
using Application.Schools.Queries.GetAll;
using Application.Schools.Queries.GetById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]
public class SchoolController : ApiController
{
    private readonly IMediator _mediator;
    public SchoolController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<GetAllSchoolsResponse>>> GetAll([FromQuery] GetAllSchoolsQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetSchoolByIdResponse>> GetById([FromRoute] int id)
    {
        var query = new GetSchoolByIdQuery(id);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateSchoolCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPut("id")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSchoolDto dto)
    {
        var command = new UpdateSchoolCommand()
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            PostalCode = dto.PostalCode,
            TelNumber = dto.TelNumber,
            Level = dto.Level,
            CityId = dto.CityId,
            ProvinceId = dto.ProvinceId
        };
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var command = new DeleteSchoolCommand(id);
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

}
