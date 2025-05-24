using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using static Application.Common.Interfaces.IFileManager;

namespace Infrastructure.Services;

public class FileManager : IFileManager
{
    public FileManager() { }

    public async Task<string> SaveFileAsync(IFormFile file, string folder, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(file.FileName);

        var fileName = file.FileName;

        var fileRelativePath = Path.Combine(Folders.Attachments, folder, fileName);


        var directoryFullPath = Path.Combine("wwwroot", Folders.Attachments, folder);

        var fileFullPath = Path.Combine(directoryFullPath, fileName);
        Directory.CreateDirectory(directoryFullPath);

        await using var fs = new FileStream(fileFullPath, FileMode.Create);
        await file.CopyToAsync(fs, cancellationToken);

        return fileRelativePath;
    }

    public void Delete(string filePath)
    {
        var path = Path.Combine("wwwroot", filePath);
        File.Delete(path);
    }

}
