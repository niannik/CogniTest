using Api.Extensions;
using Application.Common.Interfaces;
using Application.WorkingMemoryTerms.Queries.GetAll;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;


[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[Controller]")]

public class WorkingMemoryTermsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public WorkingMemoryTermsController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet("{testId:int}")]
    public async Task<ActionResult<List<GetAllWorkingMemoryTermsResponse>>> GetAll([FromRoute] int testId)
    {
        var query = new GetAllWorkingMemoryTermsQuery(testId, _currentUserService.UserId!.Value);
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
