using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = Domain.Entities.FileAggregate.File;
using static Application.Common.Interfaces.IFileManager;
using Application.WorkingMemoryTests.Common;

namespace Application.WorkingMemoryTests.Commands.UploadAudio;

public class UploadWorkingMemoryTestAudioHandler : IRequestHandler<UploadWorkingMemoryTestAudioCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileManager _fileManager;
    public UploadWorkingMemoryTestAudioHandler(IApplicationDbContext dbContext, IFileManager fileManager)
    {
        _dbContext = dbContext;
        _fileManager = fileManager;
    }
    public async Task<Result> Handle(UploadWorkingMemoryTestAudioCommand request, CancellationToken cancellationToken)
    {
        var workingMemoryTest = await _dbContext.WorkingMemoryTests
            .FirstOrDefaultAsync(x => x.Id == request.TestId, cancellationToken);

        if (workingMemoryTest is null)
            return WorkingMemoryTestErrors.WorkingMemoryTestNotFound;

        var audioPath = await _fileManager.SaveFileAsync(request.AudioFile, Folders.Audio, cancellationToken);

        var audioFile = new File(audioPath);
        _dbContext.Files.Add(audioFile);

        workingMemoryTest.Audio = audioFile;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
