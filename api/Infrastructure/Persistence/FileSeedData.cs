using File = Domain.Entities.FileAggregate.File;

namespace Infrastructure.Persistence;

public class FileSeedData
{
    public static List<File> GetFiles()
    {
        var files = new List<File>();

        files.Add(new File("Attachments/Pictures/zebra.png"));
        files.Add(new File("Attachments/Pictures/bear.png"));
        files.Add(new File("Attachments/Pictures/deer.png"));
        files.Add(new File("Attachments/Pictures/leopard.png"));
        files.Add(new File("Attachments/Pictures/elephant.png"));
        files.Add(new File("Attachments/Pictures/fox.png"));
        files.Add(new File("Attachments/Pictures/giraffe.png"));
        files.Add(new File("Attachments/Pictures/lion.png"));
        files.Add(new File("Attachments/Pictures/panda.png"));
        files.Add(new File("Attachments/Pictures/monkey.png"));
        files.Add(new File("Attachments/Pictures/rabbit.png"));

        return files;
    }
}
