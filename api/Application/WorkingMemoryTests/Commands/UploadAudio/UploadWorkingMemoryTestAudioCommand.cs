using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.WorkingMemoryTests.Commands.UploadAudio;

public record UploadWorkingMemoryTestAudioCommand : IRequest<Result>
{
    public required int TestId { get; set; }
    public required IFormFile AudioFile { get; set; }
}
