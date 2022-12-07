using System.Collections.Generic;

namespace Day07;

public class ListCommand : CommandBase
{
    public ListCommand(string[] lines)
    {
        foreach (var line in lines)
        {
            if (line[0] == 'd')
            {
                Entries.Add(new Directory(line));
            }
            else
            {
                Entries.Add(new File(line));
            }
        }
    }

    public List<FileSystemEntity> Entries { get; } = new();
    
    public override bool IsDirectoryChange => false;
}