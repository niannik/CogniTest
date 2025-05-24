using static Application.Common.Interfaces.IFileManager;
using File = Domain.Entities.FileAggregate.File;

namespace Infrastructure.Persistence;

public class FileSeedData
{
    public static List<File> GetFiles()
    {
        var files = new List<File>();
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "zebra.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "bear.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "deer.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "leopard.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "elephant.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "fox.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "giraffe.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "lion.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "panda.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "monkey.png")));
        files.Add(new File(Path.Combine(Folders.Attachments, Folders.Pictures, "rabbit.png")));

        return files;
    }
}
