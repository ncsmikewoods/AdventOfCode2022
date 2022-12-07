namespace Day07;

public class File : FileSystemEntity
{
    public File(string entryRaw)
    {
        var tokens = entryRaw.Split(" ");
        Size = int.Parse(tokens[0]);
        FileName = tokens[1];
    }
    
    public override bool IsFile { get; set; } = true;

    public string FileName { get; }

    public int Size { get; }
}