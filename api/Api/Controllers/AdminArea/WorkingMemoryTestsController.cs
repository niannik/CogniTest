using Api.Authorization;
using Api.Extensions;
using Application.WorkingMemoryTests.Commands.UploadAudio;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.SuperAdmin)]
public class WorkingMemoryTestsController : ApiController
{
    private readonly IMediator _mediator;
    public WorkingMemoryTestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("upload-audio")]
    public async Task<ActionResult> UploadAudio([FromForm] UploadWorkingMemoryTestAudioCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
