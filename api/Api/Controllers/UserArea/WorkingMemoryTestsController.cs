using Api.Extensions;
using Application.Common.Interfaces;
using Application.WorkingMemoryTests.Queries.GatAll;
using Application.WorkingMemoryTests.Queries.GetById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[Controller]")]
[Authorize]
public class WorkingMemoryTestsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public WorkingMemoryTestsController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllWorkingMemoryTestsResponse>>> GetAll()
    {
        var query = new GetAllWorkingMemoryTestsQuery(_currentUserService.UserId!.Value);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetWorkingMemoryTestByIdResponse>> GetById([FromRoute] int id)
    {
        var query = new GetWorkingMemoryTestByIdQuery(id);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
